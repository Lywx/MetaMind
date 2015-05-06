namespace MetaMind.Testimony.Screens
{
    using System;

    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

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
            this.particles = new ParticleModule(new ParticleSettings()) { SpawnRate = 2 };

            this.background = interop.Content.Load<Texture2D>(@"Textures\Screens\Background\Sea Of Mind");
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.background.Dispose();
            this.particles .Dispose();
        }

        #endregion Load and Unload

        #region Update and Draw

        public override void Draw(IGameGraphicsService graphics, GameTime time)
        {
            var spriteBatch = graphics.SpriteBatch;
            var viewport    = graphics.Manager.GraphicsDevice.Viewport;
            var fullscreen  = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch   .Draw(this.background, fullscreen, new Color(0, 0, this.TransitionAlpha / 2));
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