namespace MetaMind.Session.Guis.Screens
{
    using System;
    using Engine.Entities;
    using Engine.Screen;
    using Engine.Services;
    using Modules;

    public class SummaryScreen : MMScreen
    {
        private IMMMvcEntity summary;

        public SummaryScreen()
        {
            // Has to be a popup screen, or it can block the background
            this.IsPopup = true;
            
            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent(IMMEngineInteropService interop)
        {
            var cognition = SessionGame.SessionData.Cognition;

            this.summary = new SummaryModule(
                cognition.Consciousness,
                cognition.Synchronization,
                new SummarySettings());
            this.summary.LoadContent(interop);

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
                        this.summary.UpdateInput(input, time);
                    }
                });
        }

        public override void UnloadContent(IMMEngineInteropService interop)
        {
            base        .UnloadContent(interop);
            this.summary.UnloadContent(interop);
        }
    }
}