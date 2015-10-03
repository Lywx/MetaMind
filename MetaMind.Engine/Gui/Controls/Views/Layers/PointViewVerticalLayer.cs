namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    using Layouts;
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class PointViewVerticalLayer : PointViewLayer
    {
        public PointViewVerticalLayer(IMMViewNode view) : base(view)
        {
        }

        public new PointViewVerticalSettings ViewSettings => (PointViewVerticalSettings) base.ViewSettings;

        public new IPointViewVerticalLogic ViewLogic => (IPointViewVerticalLogic) base.ViewLogic;

        public new IPointViewVerticalSelectionController ViewSelection => this.ViewLogic.ViewSelection;

        public new IViewSwapController ViewSwap => this.ViewLogic.ViewSwap;

        public new IPointViewVerticalScrollController ViewScroll => this.ViewLogic.ViewScroll;

        public IPointViewVerticalLayout ViewLayout => this.ViewLogic.ViewLayout;
    }
}