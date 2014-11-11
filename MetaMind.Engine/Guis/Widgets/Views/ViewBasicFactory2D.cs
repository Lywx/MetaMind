using System;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public class ViewBasicFactory2D : IViewFactory
    {
        protected virtual dynamic CreateControl( IView view, ViewSettings2D viewSettings, ICloneable itemSettings )
        {
            return new ViewControl2D( view, viewSettings, itemSettings );
        }

        protected virtual IViewGraphics CreateGraphics( IView view, ViewSettings2D viewSettings, ICloneable itemSettings )
        {
            return new ViewBasicGraphics( view, viewSettings, itemSettings );
        }

        public dynamic CreateControl( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return CreateControl( view, ( ViewSettings2D ) viewSettings, itemSettings );
        }

        public IViewGraphics CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return CreateGraphics( view, ( ViewSettings2D ) viewSettings, itemSettings );
        }
    }
}