open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

for file in Directory.GetFiles(".", "*.fsx") do
    if (Path.GetFileName(file) <> __SOURCE_FILE__) then
        File.Move(file, @"Data\" + file)