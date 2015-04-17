namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Guis.Particles;
    using MetaMind.Engine.Screens;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using ParticleModule = MetaMind.Perseverance.Guis.Modules.ParticleModule;

    public class BackgroundScreen : GameScreen
    {
        private ParticleModule particles;
        
        private Texture2D      background;

        #region Constructors

        public BackgroundScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(3.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Constructors

        #region Load and Unload

        public override void LoadContent(IGameFile gameFile)
        {
            this.particles =
                new ParticleModule(
                    new ParticleModuleSettings(Perseverance.Session.Random, FloatParticle.ParticleFromSide, 8, 2));

            this.background = gameFile.Content.Load<Texture2D>(@"Textures\Screens\Background\Sea Of Mind");
        }

        public override void UnloadContent(IGameFile gameFile)
        {
            this.background.Dispose();
            this.background = null;

            this.particles.Dispose();
            this.particles = null;
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphics gameGraphics, GameTime gameTime)
        {
            var spriteBatch = gameGraphics.Screen.SpriteBatch;
            var viewport    = gameGraphics.Screen.GraphicsDevice.Viewport;
            var fullscreen  = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch   .Draw(this.background, fullscreen, new Color(0, 0, TransitionAlpha / 2));
            this.particles.Draw(gameGraphics, gameTime, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.particles.UpdateInput(gameInput, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            this.particles.Update(gameTime);
        }
        #endregion Update and Draw
    }
}