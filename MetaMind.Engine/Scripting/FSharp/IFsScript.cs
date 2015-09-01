namespace MetaMind.Engine.Scripting.FSharp
{
    public interface IFsScript : IFsScriptOperations
    {
        string Path { get; set; }
    }
}