namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointViewFactory2D : IViewFactory
    {
        public dynamic CreateControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateControl(view, (PointViewSettings2D)viewSettings, itemSettings);
        }

        public IViewGraphics CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateGraphics(view, (PointViewSettings2D)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateControl(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new PointViewControl2D(view, viewSettings, itemSettings, new ViewItemFactory2D());
        }

        protected virtual IViewGraphics CreateGraphics(IView view, PointViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new ViewGraphics(view, viewSettings, itemSettings);
        }
    }
}