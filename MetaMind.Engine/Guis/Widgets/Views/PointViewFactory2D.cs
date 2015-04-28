namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointViewFactory2D : IViewFactory
    {
        public dynamic CreateControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateLogicControl(view, (PointViewSettings2D)viewSettings, itemSettings);
        }

        public IViewVisualControl CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateVisualControl(view, (PointViewSettings2D)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateLogicControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            //return new PointView2DLogicControl(view, viewSettings, itemSettings, new ViewItemFactory2D());
            return null;
        }

        protected virtual IViewVisualControl CreateVisualControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new ViewVisualControl(view, viewSettings, itemSettings);
        }
    }
}