namespace MetaMind.Engine
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class GameVisualEntityCollection<T> : GameEntityCollection<T>
        where T : IGameVisualEntity
    {
        public void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            foreach (var module in this)
            {
                module.Draw(graphics, time, alpha);
            }
        }
    }
}