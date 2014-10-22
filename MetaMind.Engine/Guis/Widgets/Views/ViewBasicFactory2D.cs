using System;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewBasicFactory2D : IViewFactory
    {
        public virtual dynamic CreateControl( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new ViewControl2D( view, viewSettings, itemSettings );
        }

        public virtual IViewGraphics CreateGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new ViewBasicGraphics( view, viewSettings, itemSettings );
        }
    }
}