namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Engine.Screens;
    using MetaMind.Perseverance.Guis.Modules;
    using MetaMind.Perseverance.Guis.Particles;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BackgroundScreen : GameScreen
    {
        private readonly ChaosModule particles = new ChaosModule(new ChaosModuleSettings(Acutance.Adventure.Random, FloatParticle.ParticleFromBelow, 8, 2));

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
            this.particles.Update(gameTime);
            base          .Update(gameTime, otherScreenHasFocus, false);
        }

        #endregion Update and Draw
    }
}