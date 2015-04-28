namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class TraceViewFactory : PointViewFactory2D
    {
        public TraceViewFactory(ITracelist tracelist)
        {
            this.Tracelist = tracelist;
        }

        public ITracelist Tracelist { get; set; }

        protected override dynamic CreateLogicControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewLogicControl(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings, new TraceItemFactory(this.Tracelist));
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewGraphics(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings);
        }
    }
}