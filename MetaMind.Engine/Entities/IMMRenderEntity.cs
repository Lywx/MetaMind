namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMRenderEntity
    {
        RenderTarget2D RenderTarget { get; }
    }
}