namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public interface IViewFactory
    {
        dynamic CreateControl(IView view, ICloneable viewSettings, ICloneable itemSettings);

        IViewVisualControl CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings);
    }
}