namespace MetaMind.Unity.Guis.Screens
{
    using System;
    using Engine.Gui.Modules;
    using Engine.Screen;
    using Engine.Service;
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

            this.Layers.Add(new GameLayer(this)
            {
                DrawAction = (graphics, time, alpha) =>
                {
                    var rectangle = this.DestinationRectangle;
                    var color = (Color * ((float)this.TransitionAlpha / 2)).ToColor();

                    SpriteBatch.Begin();
                    SpriteBatch.Draw(this.background, rectangle, color.MakeDark(Brightness));
                    this.particles.Draw(graphics, time, this.TransitionAlpha);
                    SpriteBatch.End();
                },
                UpdateAction = time =>
                {
                    this.particles.Update(time);
                }
            });
        }

        public override void UnloadContent(IGameInteropService interop)
        {
            this.background.Dispose();
            this.particles .Dispose();
        }

        #endregion Load and Unload
    }
}