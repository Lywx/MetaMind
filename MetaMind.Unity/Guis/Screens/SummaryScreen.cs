namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using Engine;
    using Engine.Gui.Modules;
    using Engine.Screen;
    using Engine.Service;
    using Microsoft.Xna.Framework;
    using Modules;

    public class SummaryScreen : GameScreen
    {
        private IGameEntityModule summary;

        public SummaryScreen()
        {
            // Has to be a popup screen, or it can block the background
            this.IsPopup = true;
            
            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent(IGameInteropService interop)
        {
            var cognition = Unity.SessionData.Cognition;

            this.summary = new SummaryModule(
                cognition.Consciousness,
                cognition.Synchronization,
                new SummarySettings());
            this.summary.LoadContent(interop);

            this.Layers.Add(
                new GameLayer(this)
                {
                    DrawAction = (graphics, time, alpha) =>
                    {
                        SpriteBatch.Begin();
                        this.summary.Draw(graphics, time, alpha);
                        SpriteBatch.End();
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

        public override void UnloadContent(IGameInteropService interop)
        {
            base        .UnloadContent(interop);
            this.summary.UnloadContent(interop);
        }
    }
}