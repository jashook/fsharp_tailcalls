////////////////////////////////////////////////////////////////////////////////
//
// Module: comparison.fs
//
////////////////////////////////////////////////////////////////////////////////

open System
open System.IO
open System.Diagnostics

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

type Runner() =
    static member Run(program, args) =
        let proc = new Process()
        proc.StartInfo.FileName <- program
        proc.StartInfo.Arguments <- args
        proc.StartInfo.UseShellExecute <- false
        proc.StartInfo.RedirectStandardOutput <- true

        proc.Start()
        proc.WaitForExit()

        let output = proc.StandardOutput.ReadToEnd()
        let split = output.Split('-')

        Single.Parse(split.[1].Split("ms").[0])

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

[<EntryPoint>]
let main argv =
    let envVars = Environment.GetEnvironmentVariables()

    let coreRoot = Path.Join(string envVars.["CORE_ROOT"], "corerun")
    let startLocation = string envVars.["START_LOCATION"]

    // Get the baseline
    let dotnetProgram = "dotnet"

    let dotnetMs = Runner.Run(dotnetProgram, startLocation)
    let coreRootMs = Runner.Run(coreRoot, startLocation)

    //let time = Float.Parse(cleanOutput.Split("ms")[0])

    0 // return an integer exit code
