namespace MetaMind.Engine
{
    using Service;

    public interface IMMInteroperableOperations
    {
        void LoadContent(IMMEngineInteropService interop);
        
        void UnloadContent(IMMEngineInteropService interop);
    }
}