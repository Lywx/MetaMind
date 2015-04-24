namespace MetaMind.Runtime.Screens
{
    using System;

    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class SummaryScreen : GameScreen
    {
        private IModule summary;

        public SummaryScreen()
        {
            // Has to be a popup screen, or it can block the background
            this.IsPopup = true;
            
            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;

            spriteBatch.Begin();

            this.summary.Draw(graphics, time, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void LoadContent(IGameInteropService interop)
        {
            this.summary = new SummaryModule(Runtime.Session.Cognition, new SummarySettings());
            this.summary.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            base.UnloadContent(interop);

            this.summary.UnloadContent(interop);
        }

        public override void Update(GameTime gameTime)
        {
            this.summary.Update(gameTime);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.summary.UpdateInput(input, time);
        }
    }
}