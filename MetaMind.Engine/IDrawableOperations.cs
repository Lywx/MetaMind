namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Services;

    public interface IDrawableOperations
    {
        void Draw(IGameGraphicsService graphics, GameTime time, byte alpha);
    }
}
