namespace MetaMind.Engine.Gui.Controls.Views.Layers
{
    using Layouts;
    using Logic;
    using Scrolls;
    using Selections;
    using Settings;

    public class PointView2DLayer : PointViewHorizontalLayer
    {
        public PointView2DLayer(IMMViewNode view)
            : base(view)
        {
        }

        public IPointView2DLayout ViewLayout => this.ViewController.ViewLayout;

        public new IPointView2DScrollController ViewScroll => this.ViewController.ViewScroll;

        public new IPointView2DSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IMMPointView2DController ViewController => (IMMPointView2DController)base.ViewController;

        public new PointView2DSettings ViewSettings => (PointView2DSettings)base.ViewSettings;
    }
}