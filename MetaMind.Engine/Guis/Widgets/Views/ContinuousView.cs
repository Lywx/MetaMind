namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class ContinuousView : ViewEntity, IView
    {
        protected ContinuousView(ContinuousViewSettings viewSettings, ItemSettings itemSettings, IViewFactory factory)
            : base(viewSettings, itemSettings)
        {
            this.Items = new List<IViewItem>();

            this.Control  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Graphics = factory.CreateGraphics(this, viewSettings, itemSettings);
        }

        public dynamic Control { get; set; }

        public IViewGraphics Graphics { get; set; }

        public List<IViewItem> Items { get; set; }
    }
}