namespace MetaMind.Engine.Services.Script.IronPython
{
    public interface IIpyScript : IIpyScriptOperations
    {
        string Path { get; set; }
    }
}