namespace MetaMind.Engine.Gui.Controls.Views.Layouts
{
    using System;
    using System.Linq;
    using Item.Layers;

    public class IndexBlockViewVerticalLayout : BlockViewVerticalLayout
    {
        public IndexBlockViewVerticalLayout(IMMViewNode view) : base(view)
        {
        }

        public override int RowNum
        {
            get
            {
                if (this.ItemsRead.Count > 0)
                {
                    var itemLayer = this.ItemsRead.Last().GetLayer<IndexBlockViewVerticalItemLayer>();
                    var itemLayout = itemLayer.ItemLayout;
                    return itemLayout.Row + itemLayout.BlockRow + itemLayout.IndexedViewRow;
                }

                return 0;
            }
        }

        public override int RowIn(int id)
        {
            if (this.ItemsRead.Count > id)
            {
                var itemLayer = this.GetItemLayer<IndexBlockViewVerticalItemLayer>(this.ItemsRead[id]);
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.BlockRow + itemLayout.IndexedViewRow;
            }

            throw new IndexOutOfRangeException();
        }
    }
}