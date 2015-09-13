namespace MetaMind.Engine.Debugging
{
    using System;
    using Components.Fonts;
    using Microsoft.Xna.Framework;
    using Services;

    public class FrameRateCounter : DrawableGameComponent
    {
        private int frameRate;

        private int frameCounter;

        private TimeSpan elapsedTime = TimeSpan.Zero;

        public FrameRateCounter(GameEngine engine)
            : base(engine)
        {
        }

        private IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected override void LoadContent()
        {
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

            this.Graphics.StringDrawer.DrawMonospacedString(Font.UiConsole, fps, new Vector2(33, 33), Color.Black, 1f);
            this.Graphics.StringDrawer.DrawMonospacedString(Font.UiConsole, fps, new Vector2(32, 32), Color.Yellow, 1f);

            this.Graphics.SpriteBatch.End();
        }
    }
}