namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    public interface IGameGraphics
    {
        IFontDrawer FontDrawer { get; }

        GraphicsManager Graphics { get; }

        MessageManager Message { get; }

        IScreenManager Screens { get; }

        GraphicsSettings Settings { get; }
    }
}