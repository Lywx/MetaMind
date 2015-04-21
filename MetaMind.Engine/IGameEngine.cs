namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Services;

    public interface IGameEngine
    {
        IGameInput Input { get; }

        IGameInteropService Interop { get; }

        IGameGraphics Graphics { get; }
    }
}