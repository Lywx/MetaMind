namespace MetaMind.Engine.Guis.Widgets.Views.Layouts
{
    using System;
    using Items.Layers;

    public class IndexBlockViewVerticalLayout : BlockViewVerticalLayout
    {
        public IndexBlockViewVerticalLayout(IView view) : base(view)
        {
        }

        public override int RowIn(int id)
        {
            if (this.ItemsRead.Count > id)
            {
                var itemLayer = this.ItemGetLayer<IndexBlockViewVerticalItemLayer>(this.ItemsRead[id]);
                var itemLayout = itemLayer.ItemLayout;

                return itemLayout.BlockRow + itemLayout.IndexViewRow;
            }

            throw new IndexOutOfRangeException();
        }
    }
}
