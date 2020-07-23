////////////////////////////////////////////////////////////////////////////////
//
// Module: iteration_helper.fs
//
////////////////////////////////////////////////////////////////////////////////

namespace ev30

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

open System

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

type MethodCallDelegate = delegate of int -> int

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

type IterationHelper(iterations, callback: _ -> int) as this =
    let iterationCount = iterations
    let methodCallback = callback

    do this.MethodCall(iterationCount, methodCallback)
    
    member private this.MethodCall(iteration:int, cb: _ -> int) =
        if iteration > 0 then 
            cb()

            let newIterationValue = iteration - 1
            this.MethodCall(newIterationValue, cb)