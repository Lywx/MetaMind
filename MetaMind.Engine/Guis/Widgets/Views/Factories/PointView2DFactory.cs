namespace MetaMind.Engine.Guis.Widgets.Views.Factories
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public class PointView2DFactory : IViewFactory
    {
        public dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateLogicControl(view, (PointView2DSettings)viewSettings, itemSettings);
        }

        public IViewVisual CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateVisualControl(view);
        }

        protected virtual dynamic CreateLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new PointView2DLogic(view, viewSettings, itemSettings, new ViewItemFactory2D());
        }

        protected virtual IViewVisual CreateVisualControl(IView view)
        {
            return new ViewVisual(view);
        }
    }
}