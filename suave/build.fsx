#I "packages/FAKE/tools/"
#r "FakeLib.dll"

open Fake
let app     = "CustomerService"
let appDir  = "./"+app+"/bin/Debug/"
let testDir = "./Tests/bin/Debug/"

let appReferences  = 
    !! (app+"/*.fsproj")

let testReferences = 
    !! "Tests/*.fsproj"

Target "build" (fun _ ->
    MSBuildDebug appDir "Build" appReferences 
        |> Log "LibBuild-Output: "
)
Target "build_test" (fun _ ->
    MSBuildDebug testDir "Build" testReferences 
        |> Log "LibBuild-Output: "
)

Target "test" (fun _ ->  
    !! (testDir + "/Tests*.dll")
        |> NUnit (fun p -> 
            {p with
                DisableShadowCopy = true; 
                OutputFile = testDir + "TestResults.xml"})
)

Target "install" DoNothing

"build"
    ==> "build_test"

"build_test"
    ==> "test"



RunTargetOrDefault "build"
