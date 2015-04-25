namespace MetaMind.Runtime.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;
    using MetaMind.Runtime.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class MotivationScreen : GameScreen
    {
        public MotivationScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(2.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.IsPopup = true;

            this.Entities = new GameControllableEntityCollection<IModule>();
        }

        private GameControllableEntityCollection<IModule> Entities { get; set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            base.Draw(graphics, time);

            graphics.SpriteBatch.Begin();

            this.Entities.Draw(graphics, time, this.TransitionAlpha);

            graphics.SpriteBatch.End();
        }

        public override void LoadContent(IGameInteropService interop)
        {
            var cognition = Runtime.SessionData.Cognition;

            var synchronization = new SynchronizationModule(
                cognition.Consciousness,
                cognition.Synchronization,
                new SynchronizationSettings());
            synchronization.LoadContent(interop);
            this.Entities.Add(synchronization);

            //var experience = 
            //experience.LoadContent(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.Entities.UnloadContent(interop);
        }

        public override void Update(GameTime gameTime)
        {
            this.Entities.Update(gameTime);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Entities.UpdateInput(input, time);
        }
    }
}