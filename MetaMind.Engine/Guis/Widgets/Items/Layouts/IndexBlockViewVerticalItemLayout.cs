namespace MetaMind.Engine.Guis.Widgets.Items.Layouts
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

        public int IndexViewRow
        {
            get
            {
                var itemLayer = this.Item.GetLayer<IndexBlockViewVerticalItemLayer>();
                var itemLogic = itemLayer.ItemLogic;

                if (!itemLogic.IndexViewOpened)
                {
                    return 0;
                }

                var rowNum = 0;

                foreach (var item in itemLogic.IndexView.ItemsRead)
                {
                    var indexItemLayer = item.GetLayer<IndexBlockViewVerticalItemLayer>();
                    var indexItemLayout = indexItemLayer.ItemLayout;

                    rowNum += indexItemLayout.BlockRow + indexItemLayout.IndexViewRow;
                }

                return rowNum;
            }
        }

    }
}
