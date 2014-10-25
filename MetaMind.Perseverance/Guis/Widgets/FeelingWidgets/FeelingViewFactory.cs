using System;
using MetaMind.Engine.Guis.Widgets.Views;

namespace MetaMind.Perseverance.Guis.Widgets.FeelingWidgets
{
    public class FeelingViewFactory : IViewFactory
    {
        public dynamic CreateControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return new FeelingViewControl(view, viewSettings, itemSettings);
        }

        public IViewGraphics CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return new FeelingViewGraphics(view, viewSettings, itemSettings);
        }
    }
}