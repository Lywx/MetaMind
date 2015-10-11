namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Engine.Entities;
    using Engine.Screens;
    using Engine.Services;
    using Modules;

    public class SummaryScreen : MMScreen
    {
        private IMMMVCEntity summary;

        public SummaryScreen()
        {
            // Has to be a popup screen, or it can block the background
            this.IsPopup = true;
            
            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            var cognition = SessionGame.SessionData.Cognition;

            this.summary = new SummaryModule(
                cognition.Consciousness,
                cognition.Synchronization,
                new SummarySettings());
            this.summary.LoadContent();

            this.Layers.Add(
                new MMLayer(this)
                {
                    DrawAction = (graphics, time, alpha) =>
                    {
                        this.SpriteBatch.Begin();
                        this.summary.Draw(time);
                        this.SpriteBatch.End();
                    },
                    UpdateAction = time =>
                    {
                        this.summary.Update(time);
                    },
                    UpdateInputAction = (input, time) =>
                    {
                        this.summary.UpdateInput(time);
                    }
                });
        }

        public override void UnloadContent()
        {
            base        .UnloadContent();
            this.summary.UnloadContent();
        }
    }
}