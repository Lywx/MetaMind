namespace MetaMind.Engine
{
    using Service;

    public interface IInteroperableOperations
    {
        void LoadContent(IGameInteropService interop);
        
        void UnloadContent(IGameInteropService interop);
    }
}