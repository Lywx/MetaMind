namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Service;

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