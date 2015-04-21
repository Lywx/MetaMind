namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MotivationScreen : GameScreen
    {
        private readonly IModule motivation;

        private readonly IModule synchronization;

        public MotivationScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Exiting += this.MotivationScreenExiting;

            this.IsPopup = true;

            this.synchronization = new SynchronizationModule(
                Perseverance.Session.Cognition,
                new SynchronizationModuleSettings());
            this.synchronization.Load(gameFile, gameInput, gameInterop, gameSound);

            this.motivation = new MotivationModule(new MotivationModuleSettings());
            this.motivation.Load(gameFile, gameInput, gameInterop, gameSound);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            base.Draw(graphics, time);

            graphics.SpriteBatch.Begin();

            graphics.MessageDrawer.Draw(time);

            this.motivation     .Draw(graphics, time, TransitionAlpha);
            this.synchronization.Draw(graphics, time, TransitionAlpha);

            graphics.SpriteBatch.End();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            input.Event   .UpdateInput(input, time);
            input.State.UpdateInput(input, time);
            MessageManager.Update(time);
        }

        public override void Update(GameTime gameTime)
        {
            this.motivation     .Update(gameTime);
            this.synchronization.Update(gameTime);
        }

        private void MotivationScreenExiting(object sender, EventArgs e)
        {
            this.motivation     .Unload(gameFile, gameInput, gameInterop, gameSound);
            this.synchronization.Unload(gameFile, gameInput, gameInterop, gameSound);
        }
    }
}