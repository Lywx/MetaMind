namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;

    public class CommandViewFactory : PointView2DFactory
    {
        protected override dynamic CreateLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new CommandViewLogic(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings, new CommandItemFactory());
        }

        protected override IViewVisualControl CreateVisualControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new CommandViewGraphics(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings);
        }
    }
}