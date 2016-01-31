namespace MetaMind.Engine.Core.Services.Debug.Components
{
    using System;
    using Backend.Content.Fonts;
    using Microsoft.Xna.Framework;

    public class FrameRateCounter : ImmInputableComponent
    {
        private int frameRate;

        private int frameCounter;

        private MMFont frameFont;

        private TimeSpan elapsedTime = TimeSpan.Zero;

        public FrameRateCounter(MMEngine engine)
            : base(engine)
        {
        }

        protected override void LoadContent()
        {
            this.frameFont = this.GlobalInterop.Asset.Fonts["Lucida Console"];
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

            this.GlobalGraphicsRenderer.Begin();

            this.GlobalGraphicsRenderer.DrawMonospacedString(this.frameFont, fps, new Vector2(33, 33), Color.Black, 1f);
            this.GlobalGraphicsRenderer.DrawMonospacedString(this.frameFont, fps, new Vector2(32, 32), Color.Yellow, 1f);

            this.GlobalGraphicsRenderer.End();
        }
    }
}