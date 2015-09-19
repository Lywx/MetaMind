namespace MetaMind.Engine.Gui.Control.Views.Layers
{
    using Layouts;
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class PointViewVerticalLayer : PointViewLayer
    {
        public PointViewVerticalLayer(IView view) : base(view)
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

        public new IPointViewVerticalSelectionController ViewSelection
        {
            get { return this.ViewLogic.ViewSelection; }
        }

        public new IViewSwapController ViewSwap
        {
            get { return this.ViewLogic.ViewSwap; }
        }

        public new IPointViewVerticalScrollController ViewScroll
        {
            get { return this.ViewLogic.ViewScroll; }
        }

        public IPointViewVerticalLayout ViewLayout
        {
            get { return this.ViewLogic.ViewLayout; }
        }
    }
}