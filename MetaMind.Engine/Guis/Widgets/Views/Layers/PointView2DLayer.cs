namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    public class PointView2DLayer : ViewLayer
    {
        public PointView2DLayer(IView view)
            : base(view)
        {
        }

        public IPointView2DLayout ViewLayout
        {
            get
            {
                return this.ViewLogic.ViewLayout;
            }
        }

        public IPointView2DScrollControl ViewScroll
        {
            get
            {
                return this.ViewLogic.ViewScroll;
            }
        }

        public IPointView2DSelectionControl ViewSelection
        {
            get
            {
                return this.ViewLogic.ViewSelection;
            }
        }

        public new IPointView2DLogic ViewLogic
        {
            get
            {
                return (IPointView2DLogic)base.ViewLogic;
            }
        }

        public new PointView2DSettings ViewSettings
        {
            get
            {
                return (PointView2DSettings)base.ViewSettings;
            }
        }
    }
}