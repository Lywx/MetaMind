using System;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewBasicFactory1D : IViewFactory
    {
        public dynamic CreateControl( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new ViewControl1D( view, viewSettings, itemSettings );
        }

        public IViewGraphics CreateGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new ViewBasicGraphics( view, viewSettings, itemSettings );
        }
    }
}