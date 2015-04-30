namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointView1DFactory : IViewFactory
    {
        public dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateLogicControl(view, (PointViewHorizontalSettings)viewSettings, itemSettings);
        }

        public IViewVisualControl CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateVisualControl(view, (PointViewHorizontalSettings)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateLogicControl(IView view, PointViewHorizontalSettings viewSettings, ICloneable itemSettings)
        {
            //return new PointView1DLogic(view, viewSettings, itemSettings, new ViewItemFactory());
            return null;
        }

        protected virtual IViewVisualControl CreateVisualControl(IView view, PointViewHorizontalSettings viewSettings, ICloneable itemSettings)
        {
            return new ViewVisualControl(view, viewSettings, itemSettings);
        }
    }
}