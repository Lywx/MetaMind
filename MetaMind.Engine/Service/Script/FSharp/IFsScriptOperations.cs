namespace MetaMind.Engine.Service.Scripting.FSharp
{
    public interface IFsScriptOperations
    {
        void Run(FsiSession session);
    }
}