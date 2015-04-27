namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class View : ViewEntity, IView
    {
        public View(PointViewSettings1D viewSettings, ItemSettings itemSettings, IViewFactory factory)
            : this(viewSettings, itemSettings, factory)
        {
            this.Items = new List<IViewItem>(viewSettings.ColumnNumMax);
        }

        public View(PointViewSettings2D viewSettings, ItemSettings itemSettings, IViewFactory factory)
            : this(viewSettings, itemSettings, factory)
        {
            this.Items = new List<IViewItem>(viewSettings.RowNumMax * viewSettings.ColumnNumMax);
        }

        public View(ContinuousViewSettings viewSettings, ItemSettings itemSettings, IViewFactory factory)
            : this(viewSettings, itemSettings, factory)
        {
            this.Items = new List<IViewItem>();
        }

        protected View(ViewSettings viewSettings, ItemSettings itemSettings, IViewFactory factory)
            : base(viewSettings, itemSettings)
        {
            this.Control  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Graphics = factory.CreateGraphics(this, viewSettings, itemSettings);
        }


        public dynamic Control { get; set; }

        public IViewGraphics Graphics { get; set; }

        public List<IViewItem> Items { get; set; }

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