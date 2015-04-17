namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Screens;

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

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime)
        {
            var spriteBatch = gameGraphics.Screen.SpriteBatch;

            spriteBatch.Begin();

            gameGraphics.MessageDrawer.Draw(gameTime);

            this.multiplexer.Draw(gameGraphics, gameTime, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.multiplexer.UpdateInput(gameInput, gameTime);
        }

        public override void UpdateScreen(IGameGraphics gameGraphics, GameTime gameTime, bool hasOtherScreenFocus, bool isCoveredByOtherScreen)
        {
            if (this.IsActive && !isCoveredByOtherScreen)
            {
                gameGraphics.MessageDrawer.Update(gameTime);

            }

            base.UpdateScreen(gameGraphics, gameTime, hasOtherScreenFocus, isCoveredByOtherScreen);
        }

        private void MultiplexerScreenExiting(object sender, EventArgs e)
        {
            this.multiplexer.Unload(gameFile, gameInput, gameInterop, gameSound);
        }
    }
}