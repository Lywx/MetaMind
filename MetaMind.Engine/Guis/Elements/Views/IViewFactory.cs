namespace MetaMind.Engine.Guis.Elements.Views
{
    using System;

    public interface IViewFactory
    {
        dynamic       CreateControl( IView view, ICloneable viewSettings, ICloneable itemSettings );
        IViewGraphics CreateGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings );
    }
}