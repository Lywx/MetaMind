using System;
using System.Collections.Generic;

namespace MetaMind.Engine.Guis.Widgets.Views.ContinousView
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class ContinuousView1D : View
    {
        public ContinuousView1D(ICloneable viewSettings, IViewFactory viewFactory, ICloneable itemSettings)
            : base(viewSettings, viewFactory, itemSettings, new List<IViewItem>())
        {
        }
    }
}