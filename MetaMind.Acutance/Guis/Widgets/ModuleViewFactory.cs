namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class ModuleViewFactory : ViewBasicFactory2D
    {
        public ModuleViewFactory(IModulelist modulelist)
        {
            this.Modulelist = modulelist;
        }

        public IModulelist Modulelist { get; private set; }

        protected override dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new ModuleViewControl(view, (ModuleViewSettings)viewSettings, (ModuleItemSettings)itemSettings, new ModuleItemFactory(this.Modulelist));
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new TraceViewGraphics(view, (ModuleViewSettings)viewSettings, (ModuleItemSettings)itemSettings);
        }
    }
}