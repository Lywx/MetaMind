namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    using IUpdateable = Microsoft.Xna.Framework.IUpdateable;

    public class SummaryScreen : GameScreen
    {
        private readonly IModule summary;

        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryScreen"/> class.
        /// This is the most active screen of all.
        /// </summary>
        public SummaryScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Exiting += this.SummaryScreenExiting;

            this.summary = new SummaryModule(Perseverance.Session.Cognition, new SummaryModuleSettings());
            this.summary.Load(gameFile, gameInput, gameInterop, gameSound);
        }

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime)
        {
            var spriteBatch = gameGraphics.Screen.SpriteBatch;

            spriteBatch.Begin();

            gameGraphics.Message.Draw(gameTime);

            this.summary.Draw(gameGraphics, gameTime, TransitionAlpha);

            spriteBatch.End();
        }

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
            InputEventManager   .HandleInput();
            InputSequenceManager.HandleInput();

            this.summary.HandleInput();
        }

        public override void Load(IGameFile gameFile)
        {
        }

        public override void Update(IGameGraphics gameGraphics, GameTime gameTime, bool hasOtherScreenFocus, bool isCoveredByOtherScreen)
        {
            if (IsActive && !isCoveredByOtherScreen)
            {
                InputEventManager   .Update(gameTime);
                InputSequenceManager.Update(gameTime);
                MessageManager      .Update(gameTime);

                ((IUpdateable)this.summary).Update(gameTime);
            }

            base.Update(gameGraphics, gameTime, hasOtherScreenFocus, isCoveredByOtherScreen);
        }

        private void SummaryScreenExiting(object sender, EventArgs e)
        {
            this.summary.Unload(gameFile, gameInput, gameInterop, gameSound);
        }
    }
}