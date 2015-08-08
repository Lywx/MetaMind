open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let rec CopyDirectory(srcDir : string, desDir : string, excepts : seq<string>) =

    if (not <| Directory.Exists(desDir)) then ignore <| Directory.CreateDirectory(desDir) 

    Directory.GetFiles(srcDir)
    |> Seq.iter (fun (filePath) -> Path.Combine(desDir, Path.GetFileName(filePath)) |> fun (desPath) -> File.Copy(filePath, desPath, true))

    Directory.GetDirectories(srcDir)
    |> Seq.filter (fun (folderPath) -> 
        let folderName = Path.GetFileName(folderPath)
        not <| (excepts |> Seq.exists ((=) folderName)))
        

    |> Seq.iter (fun (dirPath) -> Path.Combine(desDir, Path.GetFileName(dirPath)) |> fun (desPath) -> CopyDirectory(dirPath, desPath, excepts))

CopyDirectory(@"MetaMind.Unity\bin\Debug", @"D:\Users\Meta Mind", [ "Data"; "Save"; "app.publish" ])