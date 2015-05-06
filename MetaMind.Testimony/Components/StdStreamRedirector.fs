(*
    Copyright (C) 2011 Vladimir Ivanovskiy <vivanovsky@gmail.com>.
    All rights reserved. 
    You can use this software under the terms of The Code Project Open License (CPOL) 1.02.
*)

module StdStreamRedirector

open System
open System.Runtime.InteropServices
open System.IO
open System.IO.Pipes
open Microsoft.Win32.SafeHandles
open System.Threading

module private Win32Interop = 
    [<DllImport("Kernel32.dll")>]
    extern [<MarshalAs(UnmanagedType.Bool)>] bool SetStdHandle(UInt32 nStdHandle, IntPtr hHandle)
    [<DllImport("Kernel32.dll")>]
    extern IntPtr GetStdHandle(UInt32 nStdHandle)

let private readPipe (h: SafePipeHandle, a: Action<string>) = 
    use clientPipeStream = new AnonymousPipeClientStream(PipeDirection.In, h)
    use reader = new StreamReader(clientPipeStream)
    try
        while not(reader.EndOfStream) do
            let s = reader.ReadLine()
            a.Invoke(s)
    with
        | ex -> ()
        

type Redirector(out: Action<string>, err: Action<string>) = 
    let stdOutHandleId = uint32(-11)
    let stdErrHandleId = uint32(-12)
    let pipeServerOut = new AnonymousPipeServerStream(PipeDirection.Out)
    let pipeServerErr = new AnonymousPipeServerStream(PipeDirection.Out)
    let originalStdOut = Win32Interop.GetStdHandle(stdOutHandleId)
    let originalStdErr = Win32Interop.GetStdHandle(stdErrHandleId)

    do if not(Win32Interop.SetStdHandle(stdOutHandleId, pipeServerOut.SafePipeHandle.DangerousGetHandle())) 
        then failwith "Cannot set handle for stdout."
    do if not(Win32Interop.SetStdHandle(stdErrHandleId, pipeServerErr.SafePipeHandle.DangerousGetHandle())) 
        then failwith "Cannot set handle for stderr."
    do if not(ThreadPool.QueueUserWorkItem(fun o -> readPipe(pipeServerOut.ClientSafePipeHandle, out))) 
        then failwith "Cannot run listner thread."
    do if not(ThreadPool.QueueUserWorkItem(fun o -> readPipe(pipeServerErr.ClientSafePipeHandle, err))) 
        then failwith "Cannot run listner thread."

    interface System.IDisposable with
        member x.Dispose() = 
            if not(Win32Interop.SetStdHandle(stdOutHandleId, originalStdOut)) 
                then failwith "Cannot restore original stdout."
            if not(Win32Interop.SetStdHandle(stdErrHandleId, originalStdErr)) 
                then failwith "Cannot restore original stdout."
            pipeServerOut.Dispose()
            pipeServerErr.Dispose()

let Init(stdOutAction: Action<string>, stdErrAction: Action<string>) = 
    new Redirector(stdOutAction, stdErrAction)
    

