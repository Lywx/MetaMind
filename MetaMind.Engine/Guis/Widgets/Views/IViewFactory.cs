namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public interface IViewFactory
    {
        dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings);

        IViewVisualControl CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings);
    }
}