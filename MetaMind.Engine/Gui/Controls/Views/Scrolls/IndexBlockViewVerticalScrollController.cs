namespace MetaMind.Engine.Gui.Controls.Views.Scrolls
{
    using System.Diagnostics;
    using System.Linq;
    using Item;
    using Item.Layers;

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

                var itemLayer = this.GetItemLayer(this.View.ItemsRead.Last());

                // Including the extra row owned by the indexed view
                return itemLayer.ItemLayout.Row + itemLayer.ItemLayout.BlockRow + itemLayer.ItemLayout.IndexedViewRow - this.ViewSettings.ViewRowDisplay;
            }
        }

        private IndexBlockViewVerticalItemLayer GetItemLayer(IViewItem item)
        {
            return this.GetItemLayer<IndexBlockViewVerticalItemLayer>(item);
        }
    }
}