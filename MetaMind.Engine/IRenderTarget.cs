namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework.Graphics;

    public interface IRenderTarget
    {
        RenderTarget2D RenderTarget { get; }
    }
}