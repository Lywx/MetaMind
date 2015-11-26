namespace MetaMind.Engine.Entities.Controls.Item.Layouts
{
    using Interactions;
    using Layers;

    public class MMIndexBlockViewVerticalItemLayout : MMBlockViewVerticalItemLayout, IMMIndexBlockViewVerticalItemLayout
    {
        public MMIndexBlockViewVerticalItemLayout(
            IMMViewItem item,
            IViewItemLayoutInteraction itemLayoutInteraction)
            : base(item, itemLayoutInteraction)
        {
        }

        public int IndexedViewRow
        {
            get
            {
                var itemLayer = this.Item.GetLayer<MMIndexBlockViewVerticalItemLayer>();
                var itemInteraction = itemLayer.ItemInteraction;

                if (!itemInteraction.IndexedViewOpened)
                {
                    return 0;
                }

                var rowNum = 0;

                foreach (var item in itemInteraction.IndexedView.Items)
                {
                    var indexItemLayer = item.GetLayer<MMIndexBlockViewVerticalItemLayer>();
                    var indexItemLayout = indexItemLayer.ItemLayout;

                    rowNum += indexItemLayout.BlockRow + indexItemLayout.IndexedViewRow;
                }

                return rowNum;
            }
        }

    }
}
