namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class TraceViewFactory : ViewBasicFactory2D
    {
        protected override dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewControl(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings, new TraceItemFactory());
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewGraphics(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings);
        }
    }
}