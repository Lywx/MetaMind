namespace MetaMind.Engine.Scripting.IronPython
{
    public interface IIpyScript : IIpyScriptOperations
    {
        string Path { get; set; }
    }
}