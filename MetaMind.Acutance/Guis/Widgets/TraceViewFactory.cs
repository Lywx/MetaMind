namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class TraceViewFactory : PointViewBasicFactory2D
    {
        public TraceViewFactory(ITracelist tracelist)
        {
            this.Tracelist = tracelist;
        }

        public ITracelist Tracelist { get; set; }

        protected override dynamic CreateControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewControl(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings, new TraceItemFactory(this.Tracelist));
        }

        protected override IViewGraphics CreateGraphics(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewGraphics(view, (TraceViewSettings)viewSettings, (TraceItemSettings)itemSettings);
        }
    }
}