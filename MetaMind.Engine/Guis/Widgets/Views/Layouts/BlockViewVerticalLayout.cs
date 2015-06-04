namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    using System;
    using Items.Layers;

    public class BlockViewVerticalLayout : ViewLayout, IPointViewVerticalLayout
    {
        public BlockViewVerticalLayout(IView view)
            : base(view)
        {
        }

        public int RowNum { get { return this.ItemsRead.Count; } }

        public int RowOf(int id)
        {
            if (this.ItemsRead.Count > id)
            {
                var itemLayer = this.ItemsRead[id].ItemLayer.ItemGetLayer<BlockViewVerticalItemLayer>();
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.Row;
            }

            throw new IndexOutOfRangeException();
        }

        public int RowIn(int id)
        {
            if (this.ItemsRead.Count > id)
            {
                var itemLayer = this.ItemsRead[id].ItemLayer.ItemGetLayer<BlockViewVerticalItemLayer>();
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.BlockRow;
            }

            throw new IndexOutOfRangeException();
        }
    }
}