namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class PointViewHorizontalLayer : PointViewLayer
    {
        public PointViewHorizontalLayer(IMMViewNode view)
            : base(view)
        {
        }

        public new PointViewHorizontalSettings ViewSettings => (PointViewHorizontalSettings)base.ViewSettings;

        public new IMMPointViewHorizontalController ViewController => (IMMPointViewHorizontalController)base.ViewController;

        public new IPointViewHorizontalSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IViewSwapController ViewSwap => this.ViewController.ViewSwap;

        public new IPointViewHorizontalScrollController ViewScroll => this.ViewController.ViewScroll;
    }
}