namespace Perch
open Microsoft.FSharp.Reflection
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.DerivedPatterns
open Microsoft.FSharp.Quotations.Patterns
//http://stackoverflow.com/questions/3151099/is-there-a-way-in-f-to-type-test-against-a-generic-type-without-specifying-the

    module Option=

        let (|UnionCase|_|) e o =
          match e with
          | Lambdas(_,NewUnionCase(uc,_)) | NewUnionCase(uc,[]) ->
              if (box o = null) then
                // Need special case logic in case null is a valid value (e.g. Option.None)
                let attrs = uc.DeclaringType.GetCustomAttributes(typeof<CompilationRepresentationAttribute>, false)
                if attrs.Length = 1
                   && (attrs.[0] :?> CompilationRepresentationAttribute).Flags &&& CompilationRepresentationFlags.UseNullAsTrueValue <> enum 0
                   && uc.GetFields().Length = 0
                then Some []
                else None
              else 
                let t = o.GetType()
                if FSharpType.IsUnion t then
                  let uc2, fields = FSharpValue.GetUnionFields(o,t)
                  let getGenType (t:System.Type) = if t.IsGenericType then t.GetGenericTypeDefinition() else t
                  if uc2.Tag = uc.Tag && getGenType (uc2.DeclaringType) = getGenType (uc.DeclaringType) then
                    Some(fields |> List.ofArray)
                  else None
                else None
          | _ -> failwith "The UC pattern can only be used against simple union cases"
