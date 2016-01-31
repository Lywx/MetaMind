namespace MetaMind.Engine.Core.Services.Script.FSharp
{
    public interface IFsScriptOperations
    {
        void Run(FsiSession session);
    }
}