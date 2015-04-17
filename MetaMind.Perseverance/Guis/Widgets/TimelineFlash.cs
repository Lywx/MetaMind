namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Settings.Colors;

    using Microsoft.Xna.Framework;

    using Primtives2D;

    public class FlashSettings
    {
        public Color   Color  = Palette.TransparentColor1;

        public Vector2 Position;

        public Vector2 Size = new Vector2(530, 2);

        public FlashSettings(Vector2 position)
        {
            this.Position = position;
        }
    }

    public class TimelineFlash
    {
        private FlashSettings flashSettings;

        private Color      color;

        private Vector2    position;

        private float      progress;

        private Vector2    size;

        private LightState state;

        public TimelineFlash(Vector2 position)
        {
            this.flashSettings = new FlashSettings(position);
        }

        private enum LightState
        {
            Static,
            Increment,
            Decrement,
        }

        public void Draw(GameTime gameTime, byte alpha)
        {
            Primitives2D.FillRectangle(ScreenManager.SpriteBatch, this.position.ToPoint().ToRectangleCorner(this.size.ToPoint()), this.color.MakeTransparent(alpha));
        }

        public void Update(GameTime gameTime)
        {
            if (this.state == LightState.Static && InputSequenceManager.Mouse.IsButtonLeftClicked)
            {
                this.state = LightState.Increment;
            }

            if (this.state == LightState.Increment && this.progress > 1f)
            {
                this.state = LightState.Decrement;
            }

            if (this.state == LightState.Decrement && this.progress < 0f)
            {
                this.state = LightState.Static;
            }

            if (this.state == LightState.Increment)
            {
                this.progress += 0.02f;
                this.size     += new Vector2(-10, 0);
                this.position += new Vector2(20, 0);
                this.color.R  += 3;
            }

            if (this.state == LightState.Decrement)
            {
                this.progress -= 0.02f;
                this.size     += new Vector2(10, 0);
                this.position += new Vector2(-20, 0);
                this.color.R  -= 3;
            }

            if (this.state == LightState.Static)
            {
                this.progress = 0f;
                this.size     = this.flashSettings.Size;
                this.position = this.flashSettings.Position;
                this.color    = this.flashSettings.Color;
            }
        }
    }
}