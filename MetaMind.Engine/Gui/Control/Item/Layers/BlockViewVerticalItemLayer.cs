namespace MetaMind.Engine.Gui.Control.Item.Layers
{
    using Layouts;
    using Logic;

    public class BlockViewVerticalItemLayer : PointViewVerticalItemLayer
    {
        public BlockViewVerticalItemLayer(IViewItem item)
            : base(item)
        {
        }

        public new IBlockViewVerticalItemLogic ItemLogic
        {
            get { return (IBlockViewVerticalItemLogic)base.ItemLogic; }
        }

        public new IBlockViewVerticalItemLayout ItemLayout
        {
            get { return this.ItemLogic.ItemLayout; }
        }
    }
}