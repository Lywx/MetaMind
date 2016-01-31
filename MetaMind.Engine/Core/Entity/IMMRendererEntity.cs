namespace MetaMind.Engine.Core.Entity
{
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMRendererEntity
    {
        RenderTarget2D RenderTarget { get; }
    }
}