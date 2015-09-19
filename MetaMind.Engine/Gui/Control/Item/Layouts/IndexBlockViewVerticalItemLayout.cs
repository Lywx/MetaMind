namespace MetaMind.Engine.Gui.Control.Item.Layouts
{
    using Interactions;
    using Layers;

    public class IndexBlockViewVerticalItemLayout : BlockViewVerticalItemLayout, IIndexBlockViewVerticalItemLayout
    {
        public IndexBlockViewVerticalItemLayout(
            IViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        public int IndexedViewRow
        {
            get
            {
                var itemLayer = this.Item.GetLayer<IndexBlockViewVerticalItemLayer>();
                var itemInteraction = itemLayer.ItemInteraction;

                if (!itemInteraction.IndexedViewOpened)
                {
                    return 0;
                }

                var rowNum = 0;

                foreach (var item in itemInteraction.IndexedView.ItemsRead)
                {
                    var indexItemLayer = item.GetLayer<IndexBlockViewVerticalItemLayer>();
                    var indexItemLayout = indexItemLayer.ItemLayout;

                    rowNum += indexItemLayout.BlockRow + indexItemLayout.IndexedViewRow;
                }

                return rowNum;
            }
        }

    }
}
