namespace MetaMind.Engine.Core.Entity.Control.Views.Layers
{
    using Controllers;
    using Layouts;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class MMPointViewVerticalLayer : MMPointViewLayer
    {
        public MMPointViewVerticalLayer(IMMView view) : base(view)
        {
        }

        public new PointViewVerticalSettings ViewSettings => (PointViewVerticalSettings) base.ViewSettings;

        public new IMMPointViewVerticalController ViewController => (IMMPointViewVerticalController) base.ViewController;

        public new IMMPointViewVerticalSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IMMViewSwapController ViewSwap => this.ViewController.ViewSwap;

        public new IMMPointViewVerticalScrollController ViewScroll => this.ViewController.ViewScroll;

        public IMMPointViewVerticalLayout ViewLayout => this.ViewController.ViewLayout;
    }
}