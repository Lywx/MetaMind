﻿namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;

    public class SummaryScreen : GameScreen
    {
        // TODO: Dependenency Injection
        private IModule summary;

        public SummaryScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;

            spriteBatch.Begin();

            this.summary.Draw(graphics, time, TransitionAlpha);

            spriteBatch.End();
        }

        public override void LoadContent(IGameInteropService interop)
        {
            this.summary = new SummaryModule(Perseverance.Session.Cognition, new SummaryModuleSettings());
            this.summary.Load(interop);
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            base.UnloadContent(interop);

            this.summary.UnloadContent(interop);
        }

        public override void Update(GameTime gameTime)
        {
            this.summary.Update(gameTime);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.summary.UpdateInput(input, time);
        }
    }
}