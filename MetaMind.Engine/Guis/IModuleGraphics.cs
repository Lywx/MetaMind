namespace MetaMind.Engine.Guis
{
    using Microsoft.Xna.Framework;

    public interface IModuleGraphics
    {
        void Draw(GameTime gameTime);

        void Update(GameTime gameTime);

        void Update(IGameInput gameInput, GameTime gameTime);
    }
}