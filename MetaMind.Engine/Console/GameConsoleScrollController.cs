namespace MetaMind.Engine.Console
{
    using Components.Graphics;
    using Microsoft.Xna.Framework;
    using Settings.Loaders;

    public class GameConsoleScrollController : IParameterLoader<GraphicsSettings>
    {
        private float pageUp;

        private float pageDown;

        public Vector2 ScrollOffset { get; private set; } = Vector2.Zero;

        public bool Enabled { get; private set; }

        public void LoadParameter(GraphicsSettings parameter)
        {
            this.pageUp   = parameter.Height / 2.0f;
            this.pageDown = parameter.Height / 2.0f;
        }

        public void PageUp()
        {
            this.Enabled = true;

            this.ScrollOffset += new Vector2(0, this.pageUp);
        }

        public void PageDown()
        {
            this.Enabled = true;

            this.ScrollOffset -= new Vector2(0, this.pageDown);
        }

        public void PageReset()
        {
            this.Enabled = false;

            this.ScrollOffset = Vector2.Zero;
        }
    }
}