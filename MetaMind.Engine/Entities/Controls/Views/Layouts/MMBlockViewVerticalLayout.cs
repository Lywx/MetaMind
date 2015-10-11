namespace MetaMind.Engine.Entities.Controls.Views.Layouts
{
    using System;
    using System.Linq;
    using Item.Layers;

    public class MMBlockViewVerticalLayout : MMPointViewVerticalLayout
    {
        public MMBlockViewVerticalLayout(IMMView view)
            : base(view)
        {
        }

        public override int RowNum
        {
            get
            {
                if (this.ItemsRead.Count > 0)
                {
                    var itemLayer = this.ItemsRead.Last().GetLayer<MMBlockViewVerticalItemLayer>();
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
                var itemLayer = this.GetItemLayer<MMBlockViewVerticalItemLayer>(this.ItemsRead[id]);
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.Row;
            }

            throw new IndexOutOfRangeException();
        }

        public override int RowIn(int id)
        {
            if (this.ItemsRead.Count > id)
            {
                var itemLayer = this.GetItemLayer<MMBlockViewVerticalItemLayer>(this.ItemsRead[id]);
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.BlockRow;
            }

            throw new IndexOutOfRangeException();
        }
    }
}