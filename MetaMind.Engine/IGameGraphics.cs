namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Graphics;

    public interface IGameGraphics
    {
        GraphicsManager Graphics { get; }

        ScreenManager Screen { get; }

        MessageManager Message { get; }

        FontManager Font { get; }
    }
}