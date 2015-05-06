namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class MultiplexerScreen : GameScreen
    {
        private MultiplexerModule multiplexer;

        public MultiplexerScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.IsPopup = true;
        }

        public override void LoadContent(IGameInteropService interop)
        {
            this.multiplexer = new MultiplexerModule();
            this.multiplexer.LoadContent(interop);

            base.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.multiplexer.UnloadContent(interop);

            base.UnloadContent(interop);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;

            spriteBatch.Begin();


            this.multiplexer.Draw(graphics, time, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.multiplexer.UpdateInput(input, time);
        }
    }
}