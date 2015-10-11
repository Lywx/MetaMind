namespace MetaMind.Engine.Entities.Controls.Views.Layers
{
    using Layouts;
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;

    public class MMPointView2DLayer : MMPointViewHorizontalLayer
    {
        public MMPointView2DLayer(IMMView view)
            : base(view)
        {
        }

        public IMMPointView2DLayout ViewLayout => this.ViewController.ViewLayout;

        public new IMMPointView2DScrollController ViewScroll => this.ViewController.ViewScroll;

        public new IMMPointView2DSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IMMPointView2DController ViewController => (IMMPointView2DController)base.ViewController;

        public new PointView2DSettings ViewSettings => (PointView2DSettings)base.ViewSettings;
    }
}