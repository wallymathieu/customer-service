namespace Perch
open Microsoft.FSharp.Reflection
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.DerivedPatterns
open Microsoft.FSharp.Quotations.Patterns
// Originally from stackoverflow answer
// http://stackoverflow.com/questions/3151099

    module Option=

        let (|UnionCase|_|) e o =
            let isUseNullValueAttribute (attr:obj)= 
                (attr :?> CompilationRepresentationAttribute).Flags
                     &&& CompilationRepresentationFlags.UseNullAsTrueValue <> enum 0
            let representationAttributes (uc:UnionCaseInfo)=
                uc.DeclaringType.GetCustomAttributes(typeof<CompilationRepresentationAttribute>, false)

            let getGenericType (t:System.Type)=
                if t.IsGenericType then t.GetGenericTypeDefinition() else t

            match e with
            | Lambdas(_,NewUnionCase(uc,_)) | NewUnionCase(uc,[]) ->
                if (box o = null) then
                  // Need special case logic in case null is a valid value (e.g. Option.None)
                  let attrs = representationAttributes uc
                  if attrs.Length = 1
                     && isUseNullValueAttribute attrs.[0]
                     && uc.GetFields().Length = 0
                  then Some []
                  else None
                else 
                  let t = o.GetType()
                  if FSharpType.IsUnion t then
                    let uc2, fields = FSharpValue.GetUnionFields(o,t)
                    if uc2.Tag = uc.Tag && getGenericType (uc2.DeclaringType) = getGenericType (uc.DeclaringType) then
                      Some(fields |> List.ofArray)
                    else None
                  else None
            | _ -> failwith "The UC pattern can only be used against simple union cases"