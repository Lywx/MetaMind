namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class CommandViewFactory : PointViewFactory2D
    {
        protected override dynamic CreateLogicControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new CommandViewLogicControl(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings, new CommandItemFactory());
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new CommandViewGraphics(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings);
        }
    }
}