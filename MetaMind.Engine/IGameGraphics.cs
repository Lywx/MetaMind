namespace MetaMind.Engine
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Graphics;

    public interface IGameGraphics
    {
        GraphicsManager Graphics { get; }

        IScreenManager Screen { get; }

        MessageManager Message { get; }

        IFontManager Font { get; }
    }
}