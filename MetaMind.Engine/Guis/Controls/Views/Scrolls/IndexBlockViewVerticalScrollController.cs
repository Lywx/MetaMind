namespace MetaMind.Engine.Guis.Controls.Views.Scrolls
{
    using System.Diagnostics;
    using System.Linq;
    using Items;
    using Items.Layers;

    public class IndexBlockViewVerticalScrollController : BlockViewVerticalScrollController
    {
        public IndexBlockViewVerticalScrollController(IView view) : base(view)
        {
        }

        protected override int RowOffsetMax
        {
            get
            {
#if DEBUG
                Debug.Assert(0 <= this.View.ItemsRead.Count);
#endif
                if (this.View.ItemsRead.Count == 0)
                {
                    return 0;
                }

                var itemLayer = this.ItemGetLayer(this.View.ItemsRead.Last());

                // Including the extra row owned by the indexed view
                return itemLayer.ItemLayout.Row + itemLayer.ItemLayout.BlockRow + itemLayer.ItemLayout.IndexedViewRow - this.ViewSettings.ViewRowDisplay;
            }
        }

        private IndexBlockViewVerticalItemLayer ItemGetLayer(IViewItem item)
        {
            return this.ItemGetLayer<IndexBlockViewVerticalItemLayer>(item);
        }

    }
}