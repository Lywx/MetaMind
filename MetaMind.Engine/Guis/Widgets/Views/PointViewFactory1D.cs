namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointViewFactory1D : IViewFactory
    {
        public dynamic CreateControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateControl(view, (PointViewSettings1D)viewSettings, itemSettings);
        }

        public IViewGraphics CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateGraphics(view, (PointViewSettings1D)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateControl(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings)
        {
            //return new PointViewControl1D(view, viewSettings, itemSettings, new ViewItemFactory1D());
            return null;
        }

        protected virtual IViewGraphics CreateGraphics(IView view, PointViewSettings1D viewSettings, ICloneable itemSettings)
        {
            return new ViewGraphics(view, viewSettings, itemSettings);
        }
    }
}