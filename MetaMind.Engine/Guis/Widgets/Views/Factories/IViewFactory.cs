namespace MetaMind.Engine.Guis.Widgets.Views.Factories
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public interface IViewFactory
    {
        dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings);

        IViewVisual CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings);
    }
}