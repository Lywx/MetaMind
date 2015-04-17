namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;

    public interface IUpdateable : Microsoft.Xna.Framework.IUpdateable
    {
        void UpdateContent(IGameFile gameFile, GameTime gameTime);

        void UpdateInterop(IGameInterop gameInterop, GameTime gameTime);
    }
}