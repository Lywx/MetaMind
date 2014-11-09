using System;
using MetaMind.Engine.Guis.Widgets.Views;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewFactory : IViewFactory
    {
        public dynamic CreateControl( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new MotivationViewControl( view, viewSettings, itemSettings );
        }

        public IViewGraphics CreateGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings )
        {
            return new MotivationViewGraphics( view, viewSettings, itemSettings );
        }
    }
}