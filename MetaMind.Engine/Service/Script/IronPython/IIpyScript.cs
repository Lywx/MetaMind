namespace MetaMind.Engine.Service.Scripting.IronPython
{
    public interface IIpyScript : IIpyScriptOperations
    {
        string Path { get; set; }
    }
}