namespace MetaMind.Engine.Guis.Controls
{
    using Elements;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Control : DraggableFrame, IControl
    {
        protected Control(Container container)
        {
            this.Container = container;
        }

        #region Engine Graphics Data

        protected internal readonly RenderTargetUsage RenderTargetUsage = RenderTargetUsage.DiscardContents;

        protected int ViewportHeight => this.Graphics.Settings.Height;

        protected int ViewportWidth => this.Graphics.Settings.Width;

        #endregion

        public Container Container { get; }

        #region Initialization

        public bool Initialized { get; private set; }

        public virtual void Initialize()
        {
            this.Initialized = true;
        }

        #endregion
    }
}