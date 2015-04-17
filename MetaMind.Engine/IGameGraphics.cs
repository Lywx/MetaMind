namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    public interface IGameGraphics
    {
        ITextDrawer TextDrawer { get; }

        GraphicsManager Graphics { get; }

        MessageDrawer MessageDrawer { get; }

        IScreenManager Screen { get; }

        GraphicsSettings Settings { get; }
    }
}