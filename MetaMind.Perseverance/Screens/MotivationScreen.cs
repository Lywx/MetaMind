namespace MetaMind.Runtime.Screens
{
    using System;

    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MotivationScreen : GameScreen
    {
        private IModule motivation;

        private IModule synchronization;

        public MotivationScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.IsPopup = true;
        }

        public override void LoadContent(IGameInteropService interop)
        {
            var cognition = Runtime.Session.Cognition;

            this.synchronization = new SynchronizationModule(
                cognition.Consciousness,
                cognition.Synchronization,
                new SynchronizationSettings());
            this.synchronization.LoadContent(interop);

            this.motivation = new MotivationModule(new MotivationModuleSettings());
            this.motivation.LoadContent(interop);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            base.Draw(graphics, time);

            graphics.SpriteBatch.Begin();

            this.motivation     .Draw(graphics, time, this.TransitionAlpha);
            this.synchronization.Draw(graphics, time, this.TransitionAlpha);

            graphics.SpriteBatch.End();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.motivation     .UpdateInput(input, time);
            this.synchronization.UpdateInput(input, time);
        }

        public override void Update(GameTime gameTime)
        {
            this.motivation     .Update(gameTime);
            this.synchronization.Update(gameTime);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.motivation     .UnloadContent(interop);
            this.synchronization.UnloadContent(interop);
        }
    }
}