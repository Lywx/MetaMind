namespace MetaMind.Engine.Core.Services.Script.FSharp
{
    public interface IFsScript : IFsScriptOperations
    {
        string Path { get; set; }
    }
}