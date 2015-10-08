namespace MetaMind.Engine.Services
{
    public interface IMMEngineService
    {
        IMMEngineInputService Input { get; }

        IMMEngineInteropService Interop { get; }

        IMMEngineNumericalService Numerical { get; }

        IMMEngineGraphicsService Graphics { get; }
    }
}