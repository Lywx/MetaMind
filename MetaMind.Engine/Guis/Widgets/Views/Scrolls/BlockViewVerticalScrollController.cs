namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using System.Diagnostics;
    using System.Linq;

    using Items;
    using Items.Layers;

    public class BlockViewVerticalScrollController : PointViewVerticalScrollController, IBlockViewVerticalScrollController
    {
        private int currentId;

        public BlockViewVerticalScrollController(IView view) : base(view)
        {
        }

        #region Layer

        private BlockViewVerticalItemLayer ItemGetLayer(IViewItem item)
        {
            return this.ItemGetLayer<BlockViewVerticalItemLayer>(item);
        }

        #endregion

        protected override bool CanMoveDown
        {
            get
            {
                var itemLayer = this.ItemGetLayer(this.View.ItemsRead.Last());
                return (this.ViewSettings.ViewRowDisplay + this.RowOffset) < itemLayer.ItemLayout.Row + itemLayer.ItemLayout.BlockRow;
            }
        }

        public override bool IsUpToDisplay(int id)
        {
#if DEBUG
            Debug.Assert(id >= 0);
#endif
            var itemLayer = this.ItemGetLayer(this.View.ItemsRead[id]);
            return itemLayer.ItemLayout.Row < this.RowOffset;
        }

        public override bool IsDownToDisplay(int id)
        {
#if DEBUG
            Debug.Assert(id < this.View.ItemsRead.Count);
#endif
            var itemLayer = this.ItemGetLayer(this.View.ItemsRead[id]);
            var itemLayout = itemLayer.ItemLayout;

            return itemLayout.Row + itemLayout.BlockRow > this.ViewSettings.ViewRowDisplay + this.RowOffset;
        }

        public override void MoveDown()
        {
            if (this.CanMoveDown)
            {
                var itemLayer = this.ItemGetLayer(this.View.ItemsRead[this.currentId]);
                var itemLayout = itemLayer.ItemLayout;

                this.RowOffset += itemLayout.BlockRow;

                if (this.currentId < this.View.ItemsRead.Count - 1)
                {
                    ++this.currentId;
                }
            }
        }

        public override void MoveUp()
        {
            if (this.CanMoveUp)
            {
                if (this.currentId > 0)
                {
                    --this.currentId;
                }

                var itemLayer = this.ItemGetLayer(this.View.ItemsRead[this.currentId]);
                var itemLayout = itemLayer.ItemLayout;

                this.RowOffset -= itemLayout.BlockRow;
            }
        }
    }
}