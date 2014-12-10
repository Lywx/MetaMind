namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public class ViewBasicFactory2D : IViewFactory
    {
        public dynamic CreateControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateControl(view, (ViewSettings2D)viewSettings, itemSettings);
        }

        public IViewGraphics CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateGraphics(view, (ViewSettings2D)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateControl(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new ViewControl2D(view, viewSettings, itemSettings);
        }

        protected virtual IViewGraphics CreateGraphics(IView view, ViewSettings2D viewSettings, ICloneable itemSettings)
        {
            return new ViewBasicGraphics(view, viewSettings, itemSettings);
        }
    }
}