namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    public class ViewFactory : IViewFactory
    {
        public ViewFactory(
            Func<IView, ICloneable, ICloneable, dynamic> logic,
            Func<IView, ICloneable, ICloneable, IViewVisual> visual)
        {
            if (logic == null)
            {
                throw new ArgumentNullException("logic");
            }

            if (visual == null)
            {
                throw new ArgumentNullException("visual");
            }

            this.Logic  = logic;
            this.Visual = visual;
        }

        public Func<IView, ICloneable, ICloneable, dynamic> Logic { get; set; }

        public Func<IView, ICloneable, ICloneable, IViewVisual> Visual { get; set; }

        public dynamic CreateLogicControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.Logic(view, viewSettings, itemSettings);
        }

        public IViewVisual CreateVisualControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
        {
            return this.Visual(view, viewSettings, itemSettings);
        }
    }
}