namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    using System;
    using System.Linq;
    using Item.Layers;

    public class MMIndexBlockViewVerticalLayout : MMBlockViewVerticalLayout
    {
        public MMIndexBlockViewVerticalLayout(IMMView view) : base(view)
        {
        }

        public override int RowNum
        {
            get
            {
                if (this.Items.Count > 0)
                {
                    var itemLayer = this.Items.Last().GetLayer<MMIndexBlockViewVerticalItemLayer>();
                    var itemLayout = itemLayer.ItemLayout;
                    return itemLayout.Row + itemLayout.BlockRow + itemLayout.IndexedViewRow;
                }

                return 0;
            }
        }

        public override int RowIn(int id)
        {
            if (this.Items.Count > id)
            {
                var itemLayer = this.GetItemLayer<MMIndexBlockViewVerticalItemLayer>(this.Items[id]);
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.BlockRow + itemLayout.IndexedViewRow;
            }

            throw new IndexOutOfRangeException();
        }
    }
}