namespace Perch
open System.Collections.Generic
open System.Linq

    module Dict =
        let fromSeq map list : IDictionary<'a,'b> = 
            Enumerable.ToDictionary(list, System.Func<'b,'a>(map), System.Func<'b,'b>(id)) 
                :> IDictionary<'a,'b>

        let get (hash:IDictionary<_,_>) key =
            hash.[key]
        
        let keys (hash:IDictionary<'a,_>) : 'a array =
            hash.Keys |> Seq.toArray

        let values (hash:IDictionary<_,'b>) : 'b array =
            hash.Values |> Seq.toArray

        let tryGet (hash:IDictionary<'a,'b>) (key : 'a) : 'b option =
            if hash.ContainsKey key then Some(hash.[key]) else None

        let toTuples (hash:IDictionary<'a,'b>) : ('a*'b) seq =
            let toTuple (kv:KeyValuePair<'a,'b>)=
                (kv.Key,kv.Value)
            hash |> Seq.map toTuple