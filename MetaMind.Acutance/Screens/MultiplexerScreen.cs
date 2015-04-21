namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class MultiplexerScreen : GameScreen
    {
        private readonly MultiplexerModule multiplexer;

        public MultiplexerScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Exiting += this.MultiplexerScreenExiting;

            this.IsPopup = true;

            this.multiplexer = new MultiplexerModule();
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

        public override void UpdateScreen(IGameInteropService interop, GameTime time, bool hasOtherScreenFocus, bool isCoveredByOtherScreen)
        {
            if (this.IsActive && !isCoveredByOtherScreen)
            {
                interop.MessageDrawer.Update(time);

            }

            base.UpdateScreen(interop, time, hasOtherScreenFocus, isCoveredByOtherScreen);
        }

        private void MultiplexerScreenExiting(object sender, EventArgs e)
        {
            this.multiplexer.Unload(gameFile, gameInput, gameInterop, gameSound);
        }
    }
}