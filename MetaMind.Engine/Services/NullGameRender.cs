namespace MetaMind.Engine.Services
{
    using Microsoft.Xna.Framework.Graphics;

    class NullGameRender : IGameRender
    {
        public void Initialize()
        {
            
        }

        public SpriteBatch SpriteBatch
        {
            get
            {
            }
        }
    }
}