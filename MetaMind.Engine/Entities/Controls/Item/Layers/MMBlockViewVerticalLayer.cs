namespace MetaMind.Engine.Entities.Controls.Item.Layers
{
    using Views;
    using Views.Layers;
    using Views.Logic;
    using Views.Scrolls;
    using Views.Selections;

    public class MMBlockViewVerticalLayer : MMPointViewVerticalLayer
    {
        public MMBlockViewVerticalLayer(IMMView view) : base(view)
        {
        }

        public new IMMBlockViewVerticalController ViewController => (IMMBlockViewVerticalController)base.ViewController;

        public new IMMBlockViewVerticalSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IMMBlockViewVerticalScrollController ViewScroll => this.ViewController.ViewScroll;
    }
}