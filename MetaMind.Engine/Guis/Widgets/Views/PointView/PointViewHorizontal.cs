namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointViewHorizontal : View
    {
        public PointViewHorizontal(PointViewHorizontalSettings viewSettings, ICloneable itemSettings, IViewFactory viewFactory)
            : base(viewSettings, viewFactory, itemSettings, new List<IViewItem>(viewSettings.ColumnNumMax))
        {
        }
    }
}