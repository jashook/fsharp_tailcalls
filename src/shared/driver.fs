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

    static member FirstMethod(i: int) = 
        let firstArg = 10
        let secondArg = 20
        let thirdArg = 30

        DriverMethods.FirstMethodCallee(firstArg, secondArg, thirdArg)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

type Driver() = 

    // Notes:
    //
    // Drive all of the different methods that will be called and account for
    // timing.
    member this.Start() =
        let startTime = DateTime.Now
        let obj = new IterationHelper(100, new MethodCallDelegate(DriverMethods.FirstMethod))
        let endTime = DateTime.Now

        let elapsedTime = (endTime - startTime).TotalMilliseconds

        printfn "[%s] - %fms" "FirstMethod" elapsedTime