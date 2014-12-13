namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class EventViewFactory : ViewBasicFactory2D
    {
        protected override dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new EventViewControl(view, (EventViewSettings)viewSettings, (EventItemSettings)itemSettings);
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new EventViewGraphics(view, (EventViewSettings)viewSettings, (EventItemSettings)itemSettings);
        }
    }
}