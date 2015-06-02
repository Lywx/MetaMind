namespace MetaMind.Engine.Guis.Widgets.Views.Layers
{
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class PointViewVerticalLayer : ViewLayer
    {
        protected PointViewVerticalLayer(IView view) : base(view)
        {
        }

        public new PointViewVerticalSettings ViewSettings
        {
            get { return (PointViewVerticalSettings) base.ViewSettings; }
        }

        public new IPointViewVerticalLogic ViewLogic
        {
            get { return (IPointViewVerticalLogic) base.ViewLogic; }
        }

        public IPointViewVerticalSelectionController ViewSelection
        {
            get { return this.ViewLogic.ViewSelection; }
        }

        public IPointViewVerticalSwapController ViewSwap
        {
            get { return this.ViewLogic.ViewSwap; }
        }

        public IPointViewVerticalScrollController ViewScroll
        {
            get { return this.ViewLogic.ViewScroll; }
        }
    }
}