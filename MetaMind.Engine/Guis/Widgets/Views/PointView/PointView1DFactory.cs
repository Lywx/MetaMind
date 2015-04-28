namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointView1DFactory : IViewFactory
    {
        public dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateLogicControl(view, (PointView1DSettings)viewSettings, itemSettings);
        }

        public IViewVisualControl CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateVisualControl(view, (PointView1DSettings)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateLogicControl(IView view, PointView1DSettings viewSettings, ICloneable itemSettings)
        {
            return new PointView1DLogicControl(view, viewSettings, itemSettings, new ViewItemFactory());
        }

        protected virtual IViewVisualControl CreateVisualControl(IView view, PointView1DSettings viewSettings, ICloneable itemSettings)
        {
            return new ViewVisualControl(view, viewSettings, itemSettings);
        }
    }
}