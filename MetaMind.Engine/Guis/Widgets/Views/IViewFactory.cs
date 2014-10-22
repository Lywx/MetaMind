using System;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Settings;

namespace MetaMind.Engine.Guis.Widgets.Views
{
    public interface IViewFactory
    {
        dynamic CreateControl( IView view, ICloneable viewSettings, ICloneable itemSettings );
        IViewGraphics CreateGraphics( IView view, ICloneable viewSettings, ICloneable itemSettings );
    }
}