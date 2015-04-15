namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
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

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime)
        {
            base.Draw(gameGraphics, gameTime);

            gameGraphics.Screens.SpriteBatch.Begin();

            gameGraphics.Message.Draw(gameTime);

            this.motivation     .Draw(gameGraphics, gameTime, TransitionAlpha);
            this.synchronization.Draw(gameGraphics, gameTime, TransitionAlpha);

            gameGraphics.Screens.SpriteBatch.End();
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            gameInput.Event   .UpdateInput(gameInput, gameTime);
            gameInput.Sequence.UpdateInput(gameInput, gameTime);
            MessageManager.Update(gameTime);
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