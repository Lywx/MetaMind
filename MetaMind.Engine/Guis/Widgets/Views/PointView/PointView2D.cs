namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    public class PointView2D : View
    {
        public PointView2D(PointView2DSettings viewSettings, ICloneable itemSettings, IViewFactory viewFactory)
            : base(
                viewSettings,
                viewFactory,
                itemSettings, new List<IViewItem>(viewSettings.RowNumMax * viewSettings.ColumnNumMax))
        {
        }
    }
}