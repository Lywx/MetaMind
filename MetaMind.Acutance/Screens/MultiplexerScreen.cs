namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Screens;

    using Microsoft.Xna.Framework;

    public class MultiplexerScreen : GameScreen
    {
        private readonly IModule multiplexer;

        public MultiplexerScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Exiting += this.MotivationScreenExiting;

            this.IsPopup = true;

            this.multiplexer = new MultiplexerModule(new MultiplexerModuleSettings());
            this.multiplexer.Load();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw(gameTime);

            this.multiplexer.Draw(gameTime, this.TransitionAlpha);

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager   .HandleInput();
            InputSequenceManager.HandleInput();

            this.multiplexer.HandleInput();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!coveredByOtherScreen)
            {
                Acutance.Adventure.Update();
            }

            if (this.IsActive && !coveredByOtherScreen)
            {
                InputEventManager   .Update(gameTime);
                InputSequenceManager.Update(gameTime);
                MessageManager      .Update(gameTime);

                this.multiplexer    .Update(gameTime);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void MotivationScreenExiting(object sender, EventArgs e)
        {
            this.multiplexer.Unload();
        }
    }
}