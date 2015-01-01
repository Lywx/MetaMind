namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class CommandViewFactory : ViewBasicFactory2D
    {
        protected override dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new CommandViewControl(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings, new CommandItemFactory());
        }

        protected override IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new CommandViewGraphics(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings);
        }
    }
}