namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;

    public class ModuleViewFactory : PointView2DFactory
    {
        public ModuleViewFactory(IModulelist modulelist)
        {
            this.Modulelist = modulelist;
        }

        public IModulelist Modulelist { get; private set; }

        protected override dynamic CreateLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new ModuleViewLogicControl(view, (ModuleViewSettings)viewSettings, (ModuleItemSettings)itemSettings, new ModuleItemFactory(this.Modulelist));
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new TraceViewGraphics(view, (ModuleViewSettings)viewSettings, (ModuleItemSettings)itemSettings);
        }
    }
}