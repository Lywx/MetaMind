namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointView2DFactory : IViewFactory
    {
        public dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateLogicControl(view, (PointView2DSettings)viewSettings, itemSettings);
        }

        public IViewVisualControl CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateVisualControl(view, (PointView2DSettings)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateLogicControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            //return new PointView2DLogic(view, viewSettings, itemSettings, new ViewItemFactory2D());
            return null;
        }

        protected virtual IViewVisualControl CreateVisualControl(IView view, PointView2DSettings viewSettings, ICloneable itemSettings)
        {
            return new ViewVisualControl(view, viewSettings, itemSettings);
        }
    }
}