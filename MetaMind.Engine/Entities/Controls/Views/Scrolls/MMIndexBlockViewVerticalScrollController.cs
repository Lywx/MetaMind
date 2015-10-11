namespace MetaMind.Engine.Entities.Controls.Views.Scrolls
{
    using System.Diagnostics;
    using Item;
    using Item.Layers;

    public class MMIndexBlockViewVerticalScrollController : MMBlockViewVerticalScrollController
    {
        public MMIndexBlockViewVerticalScrollController(IMMView view) : base(view)
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

        private MMIndexBlockViewVerticalItemLayer GetItemLayer(IMMViewItem item)
        {
            return this.GetItemLayer<MMIndexBlockViewVerticalItemLayer>(item);
        }
    }
}