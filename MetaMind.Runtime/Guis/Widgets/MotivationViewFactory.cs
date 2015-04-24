namespace MetaMind.Runtime.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class MotivationViewFactory : PointViewFactory1D
    {
        protected override dynamic CreateControl(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings)
        {
            return new MotivationViewControl(view, (MotivationViewSettings)viewSettings, (MotivationItemSettings)itemSettings, new MotivationItemFactory());
        }

        protected override IViewGraphics CreateGraphics(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings)
        {
            return new MotivationViewGraphics(view, (MotivationViewSettings)viewSettings, (MotivationItemSettings)itemSettings);
        }
    }
}