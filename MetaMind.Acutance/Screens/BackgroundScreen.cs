﻿namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine.Guis.Particles;
    using MetaMind.Engine.Screens;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BackgroundScreen : GameScreen
    {
        private readonly ParticleModule particles = new ParticleModule(new Engine.Guis.Modules.ParticleModuleSettings(Acutance.Adventure.Random, FloatParticle.ParticleFromBelow, 8, 2));

        private Texture2D backgroundTexture;

        #region Constructors

        public BackgroundScreen()
        {
            this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Constructors

        #region Load and Unload

        public override void LoadContent()
        {
            this.backgroundTexture = ContentManager.Load<Texture2D>(@"Textures\Screens\Background\Sea Of Mind");
        }

        public override void UnloadContent()
        {
            ContentManager.Unload();
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport    = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen  = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch   .Draw(this.backgroundTexture, fullscreen, new Color(this.TransitionAlpha, 0, 0));
            this.particles.Draw(gameTime);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            // forced input update
            this.particles.HandleInput();
            this.particles.Update(gameTime);
            base          .Update(gameTime, otherScreenHasFocus, false);
        }

        #endregion Update and Draw
    }
}