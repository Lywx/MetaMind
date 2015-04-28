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

            this.Logic  = factory.CreateControl(this, viewSettings, itemSettings);
            this.Visual = factory.CreateGraphics(this, viewSettings, itemSettings);
        }

        public dynamic Logic { get; set; }

        public IViewVisualControl Visual { get; set; }

        public List<IViewItem> Items { get; set; }
    }
}