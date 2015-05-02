namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Guis.Widgets.Views.Factories;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public class CommandViewFactory : PointView2DFactory
    {
        protected override dynamic CreateLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new CommandViewLogic(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings, new CommandItemFactory());
        }

        protected override IViewVisual CreateVisualControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new CommandViewGraphics(view, (CommandViewSettings)viewSettings, (CommandItemSettings)itemSettings);
        }
    }
}