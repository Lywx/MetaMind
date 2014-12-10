namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public class ViewBasicFactory1D : IViewFactory
    {
        public dynamic CreateControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateControl(view, (ViewSettings1D)viewSettings, itemSettings);
        }

        public IViewGraphics CreateGraphics(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.CreateGraphics(view, (ViewSettings1D)viewSettings, itemSettings);
        }

        protected virtual dynamic CreateControl(IView view, ViewSettings1D viewSettings, ICloneable itemSettings)
        {
            return new ViewControl1D(view, viewSettings, itemSettings);
        }

        protected virtual IViewGraphics CreateGraphics(IView view, ViewSettings1D viewSettings, ICloneable itemSettings)
        {
            return new ViewBasicGraphics(view, viewSettings, itemSettings);
        }
    }
}