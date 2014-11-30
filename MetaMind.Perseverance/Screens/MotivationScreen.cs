namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Guis.Widgets;
    using MetaMind.Engine.Screens;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MotivationScreen : GameScreen
    {
        private readonly IModule motivation;

        private readonly IModule synchronization;

        public MotivationScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            Exiting += this.MotivationScreenExiting;

            IsPopup = true;

            synchronization = new SynchronizationModule(
                Perseverance.Adventure.Cognition.Synchronization,
                Perseverance.Adventure.Cognition.Consciousness,
                new SynchronizationHudSettings());
            synchronization.Load();

            motivation = new MotivationExchange(new MotivationExchangeSettings());
            motivation.Load();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            ScreenManager.SpriteBatch.Begin();

            MessageManager.Draw(gameTime);

            motivation.Draw(gameTime, TransitionAlpha);
            synchronization.Draw(gameTime, TransitionAlpha);

            ScreenManager.SpriteBatch.End();
        }

        public override void HandleInput()
        {
            InputEventManager.HandleInput();
            InputSequenceManager.HandleInput();

            motivation.HandleInput();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (IsActive && !coveredByOtherScreen)
            {
                InputEventManager.Update(gameTime);
                InputSequenceManager.Update(gameTime);
                MessageManager.Update(gameTime);

                Perseverance.Adventure.Update();

                motivation.Update(gameTime);
                synchronization.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        private void MotivationScreenExiting(object sender, EventArgs e)
        {
            motivation     .Unload();
            synchronization.Unload();
        }
    }
}