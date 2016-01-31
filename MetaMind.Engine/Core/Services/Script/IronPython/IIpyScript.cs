namespace MetaMind.Engine.Core.Services.Script.IronPython
{
    public interface IIpyScript : IIpyScriptOperations
    {
        string Path { get; set; }
    }
}