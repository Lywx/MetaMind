namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;

    public class TraceViewFactory : PointView2DFactory
    {
        public TraceViewFactory(ITracelist tracelist)
        {
            this.Tracelist = tracelist;
        }

        public ITracelist Tracelist { get; set; }

        protected override dynamic CreateLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new TraceViewLogicControl(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings, new TraceItemFactory(this.Tracelist));
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new TraceViewGraphics(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings);
        }
    }
}