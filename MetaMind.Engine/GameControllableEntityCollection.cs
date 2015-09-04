namespace MetaMind.Engine
{
    using Services;

    using Microsoft.Xna.Framework;

    public class GameControllableEntityCollection<T> : GameVisualEntityCollection<T>
        where T : IGameControllableEntity
    {
        public void UpdateInput(IGameInputService input, GameTime time)
        {
            foreach (var entity in this)
            {
                entity.UpdateInput(input, time);
            }
        }
    }
}