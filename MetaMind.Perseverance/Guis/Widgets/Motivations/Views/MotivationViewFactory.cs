using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Perseverance.Guis.Widgets.Motivations.Items;
using System;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Views
{
    public class MotivationViewFactory : ViewBasicFactory1D
    {
        protected override dynamic CreateControl( IView view, ViewSettings1D viewSettings, ICloneable itemSettings )
        {
            return new MotivationViewControl( view, ( MotivationViewSettings ) viewSettings, ( MotivationItemSettings ) itemSettings );
        }

        protected override IViewGraphics CreateGraphics( IView view, ViewSettings1D viewSettings, ICloneable itemSettings )
        {
            return new MotivationViewGraphics( view, ( MotivationViewSettings ) viewSettings, ( MotivationItemSettings ) itemSettings );
        }
    }
}