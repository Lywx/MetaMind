namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class CallViewFactory : ViewBasicFactory2D
    {
        protected override dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new CallViewControl(view, (CallViewSettings)viewSettings, (CallItemSettings)itemSettings, new CallItemFactory());
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new CallViewGraphics(view, (CallViewSettings)viewSettings, (CallItemSettings)itemSettings);
        }
    }
}