namespace MetaMind.Engine.Gui.Controls.Item.Layers
{
    using Views;
    using Views.Layers;
    using Views.Logic;
    using Views.Scrolls;
    using Views.Selections;

    public class BlockViewVerticalLayer : PointViewVerticalLayer
    {
        public BlockViewVerticalLayer(IMMViewNode view) : base(view)
        {
        }

        public new IMMBlockViewVerticalController ViewController => (IMMBlockViewVerticalController)base.ViewController;

        public new IBlockViewVerticalSelectionController ViewSelection => this.ViewController.ViewSelection;

        public new IBlockViewVerticalScrollController ViewScroll => this.ViewController.ViewScroll;
    }
}