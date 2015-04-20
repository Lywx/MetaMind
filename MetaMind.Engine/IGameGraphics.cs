namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Components.Graphics;

    public interface IGameGraphics
    {
        IStringDrawer StringDrawer { get; }

        GraphicsManager Graphics { get; }

        MessageDrawer MessageDrawer { get; }

        IScreenManager Screen { get; }

        GraphicsSettings Settings { get; }
    }
}