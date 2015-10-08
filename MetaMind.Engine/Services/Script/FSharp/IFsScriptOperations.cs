namespace MetaMind.Engine.Services.Script.FSharp
{
    public interface IFsScriptOperations
    {
        void Run(FsiSession session);
    }
}