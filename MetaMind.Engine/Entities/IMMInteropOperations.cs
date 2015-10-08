namespace MetaMind.Engine.Entities
{
    using Services;

    public interface IMMInteropOperations
    {
        void LoadContent(IMMEngineInteropService interop);
        
        void UnloadContent(IMMEngineInteropService interop);
    }
}