namespace MetaMind.Engine.Guis.Widgets.Items.Layers
{
    using Views;
    using Views.Layers;
    using Views.Logic;
    using Views.Scrolls;
    using Views.Selections;

    public class BlockViewVerticalLayer : PointViewVerticalLayer
    {
        public BlockViewVerticalLayer(IView view) : base(view)
        {
        }

        public new IBlockViewVerticalLogic ViewLogic
        {
            get { return (IBlockViewVerticalLogic)base.ViewLogic; }
        }

        public new IBlockViewVerticalSelectionController ViewSelection
        {
            get { return this.ViewLogic.ViewSelection; }
        }

        public new IBlockViewVerticalScrollController ViewScroll
        {
            get { return this.ViewLogic.ViewScroll; }
        }
    }
}