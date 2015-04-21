namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;
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

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;

            spriteBatch.Begin();

            graphics.MessageDrawer.Draw(time);

            this.summary.Draw(graphics, time, TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateInput(IGameInputService input, GameTime gameTime)
        {
            this.summary.UpdateInput(input, gameTime);
        }

        public override void LoadContent(IGameFile gameFile)
        {
        }

        public override void UpdateGraphics(IGameGraphicsService graphics, GameTime gameTime)
        {
            if (this.IsActive)
            {
                graphics.MessageDrawer.Update(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.summary.Update(gameTime);
        }

        private void SummaryScreenExiting(object sender, EventArgs e)
        {
            this.summary.Unload(gameFile, gameInput, gameInterop, gameSound);
        }
    }
}