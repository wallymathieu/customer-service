#I "packages/FAKE/tools/"
#r "FakeLib.dll"

open Fake
let app     = "CustomerService"

Target "build" (fun _ ->
    MSBuildDebug "" "Build" ["CustomerService.sln"] 
        |> Log "LibBuild-Output: "
)

Target "test" (fun _ ->  
    !! (@"./*Tests/bin/Debug/*Tests*.dll")
        |> NUnit (fun p -> 
            {p with
                DisableShadowCopy = false;
            })
)

Target "install" DoNothing

"build"
    ==> "test"
   
RunTargetOrDefault "build"
