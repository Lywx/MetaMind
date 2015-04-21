namespace MetaMind.Engine.Services
{
    using Microsoft.Xna.Framework;

    public interface IGameInputableComponent : IGameComponent
    {
        void UpdateInput(GameTime gameTime);
    }
}