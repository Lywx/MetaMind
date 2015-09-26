namespace MetaMind.Engine.Gui.Controls.Views.Layouts
{
    using System;
    using System.Linq;
    using Item.Layers;

    public class BlockViewVerticalLayout : PointViewVerticalLayout
    {
        public BlockViewVerticalLayout(IView view)
            : base(view)
        {
        }

        public override int RowNum
        {
            get
            {
                if (this.ItemsRead.Count > 0)
                {
                    var itemLayer = this.ItemsRead.Last().GetLayer<BlockViewVerticalItemLayer>();
                    var itemLayout = itemLayer.ItemLayout;
                    return itemLayout.BlockRow + itemLayout.Row;
                }

                return 0;
            }
        }

        public override int RowOf(int id)
        {
            if (this.ItemsRead.Count > id)
            {
                var itemLayer = this.GetItemLayer<BlockViewVerticalItemLayer>(this.ItemsRead[id]);
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.Row;
            }

            throw new IndexOutOfRangeException();
        }

        public override int RowIn(int id)
        {
            if (this.ItemsRead.Count > id)
            {
                var itemLayer = this.GetItemLayer<BlockViewVerticalItemLayer>(this.ItemsRead[id]);
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.BlockRow;
            }

            throw new IndexOutOfRangeException();
        }
    }
}