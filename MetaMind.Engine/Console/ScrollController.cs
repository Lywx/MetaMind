namespace MetaMind.Engine.Console
{
    using Components.Graphics;
    using Microsoft.Xna.Framework;
    using Settings.Loaders;

    public class ScrollController : IParameterLoader<GraphicsSettings>
    {
        private float pageUp;

        private float pageDown;

        public ScrollController()
        {
        }

        public Vector2 ScrollOffset { get; private set; } = Vector2.Zero;

        public bool IsEnabled { get; private set; }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.pageUp   = parameter.Height / 2.0f;
            this.pageDown = parameter.Height / 2.0f;
        }

        public void PageUp()
        {
            this.IsEnabled = true;

            this.ScrollOffset += new Vector2(0, this.pageUp);
        }

        public void PageDown()
        {
            this.IsEnabled = true;

            this.ScrollOffset -= new Vector2(0, this.pageDown);
        }

        public void PageReset()
        {
            this.IsEnabled = false;

            this.ScrollOffset = Vector2.Zero;
        }
    }
}