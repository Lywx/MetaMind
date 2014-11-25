namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Screens;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MotivationScreen : GameScreen
    {
        private readonly IModule motivation;

        private readonly IModule synchronization;

        public MotivationScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.Exiting += this.MotivationScreenExiting;

            this.IsPopup = true;

            this.synchronization = new SynchronizationModule(
                Perseverance.Adventure.Cognition,
                new SynchronizationHudSettings());
            this.synchronization.Load();

            this.motivation = new MotivationExchange(new MotivationExchangeSettings());
            this.motivation.Load();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw(gameTime);

            this.motivation.Draw(gameTime, TransitionAlpha);
            this.synchronization.Draw(gameTime, TransitionAlpha);

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager.HandleInput();
            InputSequenceManager.HandleInput();

            this.motivation     .HandleInput();
            this.synchronization.HandleInput();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (IsActive && !coveredByOtherScreen)
            {
                InputEventManager   .Update(gameTime);
                InputSequenceManager.Update(gameTime);
                MessageManager      .Update(gameTime);

                Perseverance.Adventure.Update();

                motivation     .Update(gameTime);
                synchronization.Update(gameTime);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void MotivationScreenExiting(object sender, EventArgs e)
        {
            this.motivation     .Unload();
            this.synchronization.Unload();
        }
    }
}