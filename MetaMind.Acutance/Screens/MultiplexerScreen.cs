namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Engine.Screens;

    using Microsoft.Xna.Framework;

    public class MultiplexerScreen : GameScreen
    {
        private readonly MultiplexerModule multiplexer;

        public MultiplexerScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Exiting += this.MultiplexerScreenExiting;

            this.IsPopup = true;

            this.multiplexer = new MultiplexerModule();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            ScreenManager.SpriteBatch.Begin();

            MessageManager  .Draw(gameTime);

            this.multiplexer.Draw(gameTime, this.TransitionAlpha);

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager   .HandleInput();
            InputSequenceManager.HandleInput();

            this.multiplexer    .HandleInput();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (this.IsActive && !coveredByOtherScreen)
            {
                InputEventManager   .Update(gameTime);
                InputSequenceManager.Update(gameTime);
                MessageManager      .Update(gameTime);

                this.multiplexer    .Update(gameTime);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void MultiplexerScreenExiting(object sender, EventArgs e)
        {
            this.multiplexer.Unload();
        }
    }
}