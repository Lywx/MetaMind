namespace MetaMind.Engine.Gui.Control.Views.Layers
{
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class PointViewHorizontalLayer : PointViewLayer
    {
        public PointViewHorizontalLayer(IView view)
            : base(view)
        {
        }

        public new PointViewHorizontalSettings ViewSettings
        {
            get
            {
                return (PointViewHorizontalSettings)base.ViewSettings;
            }
        }

        public new IPointViewHorizontalLogic ViewLogic
        {
            get
            {
                return (IPointViewHorizontalLogic)base.ViewLogic;
            }
        }

        public new IPointViewHorizontalSelectionController ViewSelection
        {
            get
            {
                return this.ViewLogic.ViewSelection;
            }
        }

        public new IViewSwapController ViewSwap
        {
            get
            {
                return this.ViewLogic.ViewSwap;
            }
        }

        public new IPointViewHorizontalScrollController ViewScroll
        {
            get
            {
                return this.ViewLogic.ViewScroll;
            }
        }
    }
}