namespace MetaMind.Engine
{
    public interface IGameService
    {
        IGameAudio GameAudio { get; }

        IGameInput GameInput { get; }

        IGameInterop GameInterop { get; }

        IGameNumerical GameNumerical { get; }

        IGameGraphics GameGraphics { get; }

        void Provide(IGameGraphics gameGraphics);

        void Provide(IGameAudio gameAudio);

        void Provide(IGameInput gameInput);

        void Provide(IGameInterop gameInterop);
    }
}