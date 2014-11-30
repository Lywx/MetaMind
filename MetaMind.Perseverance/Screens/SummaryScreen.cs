namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Screens;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class SummaryScreen : GameScreen
    {
        private readonly IModule summary;

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryScreen"/> class.
        /// This is the most active screen of all.
        /// </summary>
        public SummaryScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            Exiting += this.SummaryScreenExiting;

            this.summary = new SummaryModule(Perseverance.Adventure.Cognition, new SummaryModuleSettings());
            this.summary.Load();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw(gameTime);

            this.summary.Draw(gameTime, TransitionAlpha);

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (IsActive && !coveredByOtherScreen)
            {
                InputEventManager.Update(gameTime);
                InputSequenceManager.Update(gameTime);
                MessageManager.Update(gameTime);

                Perseverance.Adventure.Update();

                this.summary.Update(gameTime);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void SummaryScreenExiting(object sender, EventArgs e)
        {
            this.summary.Unload();
        }
    }
}