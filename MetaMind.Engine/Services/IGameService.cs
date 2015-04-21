namespace MetaMind.Engine.Services
{
    public interface IGameService
    {
        IGameAudioService Audio { get; }

        IGameInputService Input { get; }

        IGameInteropService Interop { get; }

        IGameNumericalService Numerical { get; }

        IGameGraphicsService Graphics { get; }
    }
}