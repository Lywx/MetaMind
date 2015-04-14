namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public interface IGameVisualEntity : IDrawable  
    {
        void UpdateGraphics(IGameGraphics gameGraphics, GameTime gameTime);
    }
}