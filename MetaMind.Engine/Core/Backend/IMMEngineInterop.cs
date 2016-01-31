namespace MetaMind.Engine.Core.Backend
{
    using Services;

    public interface IMMEngineInterop : IMMGeneralComponent, IMMEngineInteropService
    {
        void OnExiting();
    }
}
