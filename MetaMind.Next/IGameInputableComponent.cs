namespace MetaMind.Next
{
    using Microsoft.Xna.Framework;

    public interface IGameInputableComponent : IGameComponent
    {
        void UpdateInput(GameTime gameTime);
    }
}