namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Service;

    // TODO(Minor): I don't really need this. The design of collection is not very convenient on universal update/draw/... Maybe I should clean up with empty virtual method in collection?

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