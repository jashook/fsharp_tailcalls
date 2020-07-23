////////////////////////////////////////////////////////////////////////////////
//
// Module: driver.fs
//
////////////////////////////////////////////////////////////////////////////////

namespace ev30

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

open System

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

type DriverMethods() =
    static member FirstMethodCallee(firstArg: int, secondArg: int, thirdArg: int) = 
        if firstArg = 10 then firstArg * secondArg * thirdArg
        else firstArg + secondArg + thirdArg

    static member FirstMethod() = 
        let firstArg = 10
        let secondArg = 20
        let thirdArg = 30

        DriverMethods.FirstMethodCallee(firstArg, secondArg, thirdArg)

    static member SecondMethodCallee(iteration: int, firstArg: int, secondArg: int, thirdArg: int) = 
        if iteration > 0 then
            if firstArg = 10 then firstArg * secondArg * thirdArg
            else firstArg + secondArg + thirdArg

            let newIteration = iteration - 1
            DriverMethods.SecondMethodCallee(newIteration, firstArg, secondArg, thirdArg)


    static member SecondMethod() = 
        let firstArg = 10
        let secondArg = 20
        let thirdArg = 30

        DriverMethods.SecondMethodCallee(100, firstArg, secondArg, thirdArg)

        0

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

type Driver() = 

    // Notes:
    //
    // Drive all of the different methods that will be called and account for
    // timing.
    member this.Start() =
        let rec runFirstMethod iterationCount = 
            match iterationCount with
            | 0 -> 0
            | x when x > 0 ->
                DriverMethods.FirstMethod()
                runFirstMethod(iterationCount - 1)
                0

        let firstStartTime = DateTime.Now
        runFirstMethod 100000
        let firstEndTime = DateTime.Now

        let firstElapsedTime = (firstEndTime - firstStartTime).TotalMilliseconds

        printfn "[%s] - %fms" "FirstMethod" firstElapsedTime

        let secondStartTime = DateTime.Now
        let secondObj = new IterationHelper(10000000, DriverMethods.SecondMethod)
        let secondEndTime = DateTime.Now

        let secondElapsedTime = (secondEndTime - secondStartTime).TotalMilliseconds

        printfn "[%s] - %fms" "SecondMethod" secondElapsedTime