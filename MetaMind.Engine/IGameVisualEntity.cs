namespace MetaMind.Engine
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IGameVisualEntity : IDrawable  
    {
        void UpdateGraphics(IGameGraphicsService graphics, GameTime time);
    }
}