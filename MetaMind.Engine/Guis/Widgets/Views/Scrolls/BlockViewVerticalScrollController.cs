namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using System.Diagnostics;
    using System.Linq;

    using Items;
    using Items.Layers;

    public class BlockViewVerticalScrollController : PointViewVerticalScrollController, IBlockViewVerticalScrollController
    {
        private int currentId;

        private int rowOffset;

        public BlockViewVerticalScrollController(IView view) : base(view)
        {
        }

        #region Layer

        private BlockViewVerticalItemLayer ItemGetLayer(IViewItem item)
        {
            return this.ItemGetLayer<BlockViewVerticalItemLayer>(item);
        }

        #endregion

        #region State

        public override int RowOffset
        {
            get { return this.rowOffset; }
            set
            {
                if (value < this.RowOffsetMin)
                {
                    this.rowOffset = this.RowOffsetMin;
                }
                else if (value > this.RowOffsetMax)
                {
                    this.rowOffset = this.RowOffsetMax;
                }
                else
                {
                    this.rowOffset = value;
                }
            }
        }

        protected override int RowOffsetMax
        {
            get
            {
#if DEBUG
                Debug.Assert(0 < this.View.ItemsRead.Count);
#endif
                var itemLayer = this.ItemGetLayer(this.View.ItemsRead.Last());
                return itemLayer.ItemLayout.Row + itemLayer.ItemLayout.BlockRow - this.ViewSettings.ViewRowDisplay;
            }
        }

        #endregion

        #region Display

        protected override bool CanMoveDown
        {
            get
            {
                return this.RowOffset < this.RowOffsetMax;
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


        #endregion

        #region Operations

        public override void MoveDown()
        {
            // Items is empty
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

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
            // Items is empty
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

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

        #endregion
    }
}