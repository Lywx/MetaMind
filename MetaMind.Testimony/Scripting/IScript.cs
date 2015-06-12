namespace MetaMind.Testimony.Scripting
{
    public interface IScript : IScriptOperations
    {
        string Path { get; set; }
    }
}