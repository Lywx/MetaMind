namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointView : ViewEntity, IView
    {
        public PointView(PointViewSettings1D viewSettings, ItemSettings itemSettings, IViewFactory factory, dynamic parent = null)
            : base(viewSettings, itemSettings)
        {
            this.Items = new List<IViewItem>(viewSettings.ColumnNumMax);

            this.Control  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Graphics = factory.CreateGraphics(this, viewSettings, itemSettings);

            this.Parent = parent;
        }

        public PointView(PointViewSettings2D viewSettings, ItemSettings itemSettings, IViewFactory factory, dynamic parent = null)
            : base(viewSettings, itemSettings)
        {
            this.Items = new List<IViewItem>(viewSettings.RowNumMax * viewSettings.ColumnNumMax);

            this.Control  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Graphics = factory.CreateGraphics(this, viewSettings, itemSettings);

            this.Parent = parent;
        }

        public dynamic Control { get; set; }

        public IViewGraphics Graphics { get; set; }

        public List<IViewItem> Items { get; set; }

        public dynamic Parent { get; private set; }

        public override void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
            this.Graphics.Draw(graphics, time, alpha);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.Control.Update(input, time);
        }

        public override void Update(GameTime time)
        {
            this.Control .Update(time);
            this.Graphics.Update(time);
        }
    }
}