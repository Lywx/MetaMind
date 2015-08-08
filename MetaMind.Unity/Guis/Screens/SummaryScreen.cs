namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using Engine.Guis;
    using Engine.Screens;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Modules;

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
            var cognition = Unity.SessionData.Cognition;

            this.summary = new SummaryModule(
                cognition.Consciousness,
                cognition.Synchronization,
                new SummarySettings());
            this.summary.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            base        .UnloadContent(interop);
            this.summary.UnloadContent(interop);
        }

        public override void Update(GameTime time)
        {
            this.summary.Update(time);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.summary.UpdateInput(input, time);
        }
    }
}