namespace MetaMind.Engine.Service
{
    public interface IMMEngineService
    {
        IMMEngineInputService Input { get; }

        IMMEngineInteropService Interop { get; }

        IMMEngineNumericalService Numerical { get; }

        IMMEngineGraphicsService Graphics { get; }
    }
}