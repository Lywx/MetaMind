namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public interface IUpdateable : Microsoft.Xna.Framework.IUpdateable
    {
        void Update(IGameFile gameFile, GameTime gameTime);

        void Update(IGameInterop gameInterop, GameTime gameTime);

        void Update(IGameSound gameSound, GameTime gameTime);
    }
}