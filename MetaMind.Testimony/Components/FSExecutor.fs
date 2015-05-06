(*
    Copyright (C) 2011 Vladimir Ivanovskiy <vivanovsky@gmail.com>.
    All rights reserved. 
    You can use this software under the terms of The Code Project Open License (CPOL) 1.02.
*)

module FSExecutor

open System
open System.Text
open System.Diagnostics
open System.Threading
open System.Collections.Generic

open System.Reflection
open System.CodeDom.Compiler
open FSharp.Compiler.CodeDom
open System.Security.Cryptography

let mutable private CompiledAssemblies = new Dictionary<string, Assembly>()

let getMd5Hash (code: string) = 
    let md5 = MD5.Create()
    let codeBytes = Encoding.UTF8.GetBytes(code)
    let hash = md5.ComputeHash(codeBytes)
    let sb = new StringBuilder()
    for b in hash do sb.Append(b.ToString("x2")) |> ignore done
    sb.ToString()

let compile (code: string) references = 
    let compiler = new FSharpCodeProvider()
    let cp = new System.CodeDom.Compiler.CompilerParameters()
    for r in references do cp.ReferencedAssemblies.Add(r) |> ignore done
    cp.GenerateInMemory <- true
    cp.GenerateExecutable <- true
    cp.WarningLevel <- 4
    cp.IncludeDebugInformation <- false
    cp.CompilerOptions <- "-O"
    let cr = compiler.CompileAssemblyFromSource(cp, code)
    (cr.CompiledAssembly, cr.Output, cr.Errors)

let executeAssembly (a: Assembly) = 
    try
        a.EntryPoint.Invoke(null, null) |> ignore
        printfn "Execution successfully completed."
    with
        | :? TargetInvocationException as tex -> eprintfn "Execution failed with: %s" (tex.InnerException.Message)
        | ex -> eprintfn "Execution cannot start, reason: %s" (ex.ToString())


let CompileAndExecute(code: string, references: seq<string>) = 
    let sw = new Stopwatch()
    sw.Start()
    let hash = getMd5Hash code
    if CompiledAssemblies.ContainsKey(hash) then
        executeAssembly CompiledAssemblies.[hash]
    else
        let (assembly, output, errors) = compile code references
        if errors.Count > 0 then
            for e in errors do eprintfn "%s" (e.ToString()) done
        else
            for o in output do printfn "%s" o done
            executeAssembly assembly
            CompiledAssemblies.Add(hash, assembly)
    sw.Stop()
    printfn "%s %i milliseconds." "Compile and execute takes" sw.ElapsedMilliseconds
    

