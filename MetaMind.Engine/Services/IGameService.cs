namespace MetaMind.Engine.Services
{
    public interface IGameService
    {
        IGameInputService Input { get; }

        IGameInteropService Interop { get; }

        IGameNumericalService Numerical { get; }

        IGameGraphicsService Graphics { get; }
    }
}