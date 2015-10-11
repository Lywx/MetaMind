namespace MetaMind.Engine.Entities.Controls.Views.Layers
{
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;
    using Swaps;

    public class MMPointViewHorizontalLayer : MMPointViewLayer
    {
        public MMPointViewHorizontalLayer(IMMView view)
            : base(view)
        {
        }

        public new PointViewHorizontalSettings ViewSettings => (PointViewHorizontalSettings)base.ViewSettings;

        public new IMMPointViewHorizontalController ViewController => (IMMPointViewHorizontalController)base.ViewController;

        public new IMMPointViewHorizontalSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IMMViewSwapController ViewSwap => this.ViewController.ViewSwap;

        public new IMMPointViewHorizontalScrollController ViewScroll => this.ViewController.ViewScroll;
    }
}