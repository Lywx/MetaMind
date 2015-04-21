namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Acutance.Guis.Modules;
    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Particles;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BackgroundScreen : GameScreen
    {
        private readonly ParticleModule particles = new ParticleModule(new Engine.Guis.Modules.ParticleModuleSettings(Acutance.Session.Random, FloatParticle.ParticleFromBelow, 4, 2));

        private Texture2D backgroundTexture;

        #region Constructors

        public BackgroundScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Constructors

        #region Load and Unload

        public override void LoadContent(IGameFile gameFile)
        {
            this.backgroundTexture = gameFile.Content.Load<Texture2D>(@"Textures\Screens\Background\Sea Of Mind");
        }

        public override void UnloadContent(IGameFile gameFile)
        {
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;
            var viewport    = graphics.Manager.GraphicsDevice.Viewport;
            var fullscreen  = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(this.backgroundTexture, fullscreen, new Color(210, 100, 95).MakeTransparent(TransitionAlpha));

            this.particles.Draw(graphics, time, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateScreen(IGameGraphicsService graphics, GameTime gameTime, bool hasOtherScreenFocus, bool isCoveredByOtherScreen)
        {
            this.particles.Update(gameTime);
            base          .UpdateScreen(graphics, gameTime, hasOtherScreenFocus, false);
        }

        #endregion Update and Draw
    }
}