namespace MetaMind.Engine.Entities
{
    using Service;

    public interface IMMInteropOperations
    {
        void LoadContent(IMMEngineInteropService interop);
        
        void UnloadContent(IMMEngineInteropService interop);
    }
}