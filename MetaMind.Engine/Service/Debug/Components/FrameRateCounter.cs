namespace MetaMind.Engine.Service.Debugging.Components
{
    using System;
    using Engine.Components.Content.Fonts;
    using Microsoft.Xna.Framework;

    public class FrameRateCounter : GameInputableComponent
    {
        private int frameRate;

        private int frameCounter;

        private Font frameFont;

        private TimeSpan elapsedTime = TimeSpan.Zero;

        public FrameRateCounter(GameEngine engine)
            : base(engine)
        {
        }

        protected override void LoadContent()
        {
            this.frameFont = this.Interop.Asset.Fonts["Lucida Console"];
        }

        protected override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.elapsedTime += gameTime.ElapsedGameTime;

            if (this.elapsedTime > TimeSpan.FromSeconds(1))
            {
                this.elapsedTime -= TimeSpan.FromSeconds(1);
                this.frameRate = this.frameCounter;
                this.frameCounter = 0;
            }
        }


        public override void Draw(GameTime gameTime)
        {
            this.frameCounter++;

            var fps = $"FPS: {this.frameRate}";

            this.Graphics.SpriteBatch.Begin();

            this.Graphics.Renderer.DrawMonospacedString(this.frameFont, fps, new Vector2(33, 33), Color.Black, 1f);
            this.Graphics.Renderer.DrawMonospacedString(this.frameFont, fps, new Vector2(32, 32), Color.Yellow, 1f);

            this.Graphics.SpriteBatch.End();
        }
    }
}