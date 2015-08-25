namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using Engine.Guis.Modules;
    using Engine.Screens;
    using Engine.Services;
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

        #region Static Properties 

        public static float Brightness { get; set; } = 1f;

        public static Vector3 Color { get; set; } = new Vector3(0, 0, 1);

        #endregion

        #region Load and Unload

        public override void LoadContent(IGameInteropService interop)
        {
            this.particles = new ParticleModule(new ParticleSettings()) { SpawnRate = 1 };

            this.background = interop.Content.Load<Texture2D>(@"Texture\Backgrounds\Sea Of Mind");
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

            spriteBatch   .Draw(this.background, fullscreen, (Color * ((float)this.TransitionAlpha / 2)).ToColor().WithBrightness(Brightness));
            this.particles.Draw(graphics, time, this.TransitionAlpha);

            spriteBatch.End();
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.particles.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            this.particles.Update(time);
        }
        #endregion Update and Draw
    }
}