namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Services;

    public interface IGameEngine
    {
        IGameInput Input { get; }

        IGameInterop Interop { get; }

        IGameGraphics Graphics { get; }

        void Run();
    }
}