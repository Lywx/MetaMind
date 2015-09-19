namespace MetaMind.Engine.Gui.Control.Views.Layers
{
    using Layouts;
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;

    public class PointView2DLayer : PointViewHorizontalLayer
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

        public new IPointView2DScrollController ViewScroll
        {
            get
            {
                return this.ViewLogic.ViewScroll;
            }
        }

        public new IPointView2DSelectionController ViewSelection
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