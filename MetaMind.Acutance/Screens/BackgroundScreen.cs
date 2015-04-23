namespace MetaMind.Acutance.Screens
{
    using System;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Modules;
    using MetaMind.Engine.Guis.Particles;
    using MetaMind.Engine.Screens;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using ParticleModule = MetaMind.Acutance.Guis.Modules.ParticleModule;

    public class BackgroundScreen : GameScreen
    {
        private ParticleModule particles;

        private Texture2D background;

        #region Constructors

        public BackgroundScreen()
        {
            this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
            this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        #endregion Constructors

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.background = interop.Content.Load<Texture2D>(@"Textures\Screens\Background\Sea Of Mind");
            
            this.particles =
                new ParticleModule(
                    new ParticleSettings(Acutance.Session.Random, FloatParticle.ParticleFromBelow, 4, 2))
        }

        public override void UnloadContent(IGameInteropService interop)
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

            spriteBatch   .Draw(this.background, fullscreen, new Color(210, 100, 95).MakeTransparent(TransitionAlpha));
            this.particles.Draw(graphics, time, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateScreen(IGameInteropService interop, GameTime time, bool hasOtherScreenFocus, bool isCoveredByOtherScreen)
        {
            this.particles.Update(time);
            base          .UpdateScreen(interop, time, hasOtherScreenFocus, false);
        }

        #endregion Update and Draw
    }
}