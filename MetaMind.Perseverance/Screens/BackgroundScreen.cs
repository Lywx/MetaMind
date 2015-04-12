namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Particles;
    using MetaMind.Engine.Screens;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BackgroundScreen : GameScreen
    {
        private readonly ParticleModule particles = new ParticleModule(new Engine.Guis.Modules.ParticleModuleSettings(Perseverance.Session.Random, FloatParticle.ParticleFromSide, 8, 2));
        private          Texture2D      backgroundTexture;

        #region Constructors

        public BackgroundScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(3.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Constructors

        #region Load and Unload

        public override void Load(IGameFile gameFile)
        {
            this.backgroundTexture = gameFile.Content.Load<Texture2D>(@"Textures\Screens\Background\Sea Of Mind");
        }

        public override void Unload(IGameFile gameFile)
        {
            this.backgroundTexture.Dispose();
            this.backgroundTexture = null;
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime)
        {
            var spriteBatch = gameGraphics.Screen.SpriteBatch;
            var viewport    = gameGraphics.Screen.GraphicsDevice.Viewport;
            var fullscreen  = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch   .Draw(backgroundTexture, fullscreen, new Color(0, 0, TransitionAlpha / 2));
            this.particles.Draw(gameGraphics, gameTime, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void Update(IGameInput gameInput, GameTime gameTime)
        {
            this.particles.Update(gameInput, gameTime);
            this.particles.Update(gameTime);
        }

        #endregion Update and Draw
    }
}