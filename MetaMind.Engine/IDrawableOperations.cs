namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Service;

    public interface IDrawableOperations
    {
        void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha);

        void Draw(IGameGraphicsService graphics, GameTime time, byte alpha);

        void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha);
    }
}
