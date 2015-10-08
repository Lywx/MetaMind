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

        public new IMMPointViewVerticalController ViewController => (IMMPointViewVerticalController) base.ViewController;

        public new IPointViewVerticalSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IViewSwapController ViewSwap => this.ViewController.ViewSwap;

        public new IPointViewVerticalScrollController ViewScroll => this.ViewController.ViewScroll;

        public IPointViewVerticalLayout ViewLayout => this.ViewController.ViewLayout;
    }
}