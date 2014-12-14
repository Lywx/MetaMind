namespace MetaMind.Perseverance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class MotivationViewFactory : ViewBasicFactory1D
    {
        protected override dynamic CreateControl(IView view, ViewSettings1D viewSettings, ICloneable itemSettings)
        {
            return new MotivationViewControl(view, (MotivationViewSettings)viewSettings, (MotivationItemSettings)itemSettings, new MotivationItemFactory());
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings1D viewSettings, ICloneable itemSettings)
        {
            return new MotivationViewGraphics(view, (MotivationViewSettings)viewSettings, (MotivationItemSettings)itemSettings);
        }
    }
}