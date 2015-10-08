namespace MetaMind.Engine.Services.Script.FSharp
{
    public interface IFsScript : IFsScriptOperations
    {
        string Path { get; set; }
    }
}