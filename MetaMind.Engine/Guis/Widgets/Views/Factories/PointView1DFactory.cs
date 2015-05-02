namespace MetaMind.Engine.Guis.Widgets.Views.Factories
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;

    public class PointView1DFactory : IViewFactory
    {
        public dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateLogicControl(view, (PointViewHorizontalSettings)viewSettings, itemSettings);
        }

        public IViewVisual CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateVisualControl(view, (PointViewHorizontalSettings)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateLogicControl(IView view, PointViewHorizontalSettings viewSettings, ICloneable itemSettings)
        {
            //return new PointViewHorizontalLogic(view, viewSettings, itemSettings, new ViewItemFactory());
            return null;
        }

        protected virtual IViewVisual CreateVisualControl(IView view, PointViewHorizontalSettings viewSettings, ICloneable itemSettings)
        {
            return new ViewVisual(view, viewSettings, itemSettings);
        }
    }
}