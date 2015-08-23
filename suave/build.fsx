#I "packages/FAKE/tools/"
#r "FakeLib.dll"

open Fake
let app     = "CustomerService"

Target "build" (fun _ ->
    MSBuildDebug "" "Build" ["CustomerService.sln"] 
        |> Log "LibBuild-Output: "
)

Target "test_customerservice" (fun _ ->  
    !! (@"./Tests/bin/Debug/*Tests*.dll")
        |> NUnit (fun p -> 
            {p with
                DisableShadowCopy = false;
            })
)

Target "test_billingservice" (fun _ ->  
    !! (@"./BillingService.Tests/bin\Debug/*Tests*.dll")
        |> NUnit (fun p -> 
            {p with
                DisableShadowCopy = false;
            })
)


Target "install" DoNothing

Target "test" DoNothing

"test_billingservice"
    ==> "test"
    ==> "build"
"test_customerservice" 
    ==> "test"
    ==> "build"

RunTargetOrDefault "build"
