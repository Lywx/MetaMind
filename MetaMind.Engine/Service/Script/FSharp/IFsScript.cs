namespace MetaMind.Engine.Service.Scripting.FSharp
{
    public interface IFsScript : IFsScriptOperations
    {
        string Path { get; set; }
    }
}