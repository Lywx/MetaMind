namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class ModuleViewFactory : PointViewFactory2D
    {
        public ModuleViewFactory(IModulelist modulelist)
        {
            this.Modulelist = modulelist;
        }

        public IModulelist Modulelist { get; private set; }

        protected override dynamic CreateLogicControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new ModuleViewLogicControl(view, (ModuleViewSettings)viewSettings, (ModuleItemSettings)itemSettings, new ModuleItemFactory(this.Modulelist));
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewGraphics(view, (ModuleViewSettings)viewSettings, (ModuleItemSettings)itemSettings);
        }
    }
}