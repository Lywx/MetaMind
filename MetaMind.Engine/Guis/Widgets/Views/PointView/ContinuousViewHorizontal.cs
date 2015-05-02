namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    public class ContinuousViewHorizontal : View
    {
        public ContinuousViewHorizontal(ContinuousViewHorizontalSettings viewSettings, IViewFactory viewFactory, ItemSettings itemSettings)
            : base(viewSettings, viewFactory, itemSettings, new List<IViewItem>())
        {
        }
    }
}