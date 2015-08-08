namespace MetaMind.Unity.Scripting
{
    public interface IScript : IScriptOperations
    {
        string Path { get; set; }
    }
}