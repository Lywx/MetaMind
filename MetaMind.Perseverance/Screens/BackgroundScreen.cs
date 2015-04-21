namespace MetaMind.Perseverance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Guis.Particles;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using IGameInteropService = MetaMind.Engine.IGameInteropService;
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

        public override void LoadContent(IGameInteropService interop)
        {
            this.particles =
                new ParticleModule(
                    new ParticleModuleSettings(Perseverance.Session.Random, FloatParticle.ParticleFromSide, 8, 2));

            this.background = interop.Content.Load<Texture2D>(@"Textures\Screens\Background\Sea Of Mind");
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.background.Dispose();
            this.background = null;

            this.particles.Dispose();
            this.particles = null;
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;
            var viewport    = graphics.Manager.GraphicsDevice.Viewport;
            var fullscreen  = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch   .Draw(this.background, fullscreen, new Color(0, 0, TransitionAlpha / 2));
            this.particles.Draw(graphics, time, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.particles.UpdateInput(input, time);
        }

        public override void Update(GameTime gameTime)
        {
            this.particles.Update(gameTime);
        }
        #endregion Update and Draw
    }
}