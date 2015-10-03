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

        public new IBlockViewVerticalLogic ViewLogic => (IBlockViewVerticalLogic)base.ViewLogic;

        public new IBlockViewVerticalSelectionController ViewSelection => this.ViewLogic.ViewSelection;

        public new IBlockViewVerticalScrollController ViewScroll => this.ViewLogic.ViewScroll;
    }
}