namespace MetaMind.Engine
{
    using Services;

    public interface IInteroperableOperations
    {
        void LoadContent(IGameInteropService interop);
        
        void UnloadContent(IGameInteropService interop);
    }
}