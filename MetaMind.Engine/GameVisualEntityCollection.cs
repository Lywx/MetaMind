namespace MetaMind.Engine
{
    using Services;
    using Microsoft.Xna.Framework;

    public class GameVisualEntityCollection<T> : GameEntityCollection<T>
        where T : IGameVisualEntity
    {
        public void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                entity.BeginDraw(graphics, time, alpha);
            }
        }

        public void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                entity.Draw(graphics, time, alpha);
            }
        }

        public void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var entity in this)
            {
                entity.EndDraw(graphics, time, alpha);
            }
        }
    }
}