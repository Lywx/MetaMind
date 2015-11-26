namespace MetaMind.Engine.Entities.Controls.Views.Scrolls
{
    using System.Diagnostics;
    using Item;
    using Item.Layers;

    public class MMBlockViewVerticalScrollController : MMPointViewVerticalScrollController, IMMBlockViewVerticalScrollController
    {
        private int currentId;

        private int rowOffset;

        public MMBlockViewVerticalScrollController(IMMView view) : base(view)
        {
        }

        #region Layer

        private MMBlockViewVerticalItemLayer GetItemLayer(IMMViewItem item)
        {
            return this.GetItemLayer<MMBlockViewVerticalItemLayer>(item);
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
                Debug.Assert(0 <= this.View.Items.Count);
#endif
                if (this.View.Items.Count == 0)
                {
                    return 0;
                }

                var itemLayer = this.GetItemLayer(this.View.Items.Last());
                return itemLayer.ItemLayout.Row + itemLayer.ItemLayout.BlockRow - this.ViewSettings.ViewRowDisplay;
            }
        }

        #endregion

        #region Display

        protected override bool CanMoveDown => this.RowOffset < this.RowOffsetMax;

        public override bool IsUpToDisplay(int id)
        {
#if DEBUG
            Debug.Assert(id >= 0);
#endif
            var itemLayer = this.GetItemLayer(this.View.Items[id]);
            return itemLayer.ItemLayout.Row < this.RowOffset;
        }

        public override bool IsDownToDisplay(int id)
        {
#if DEBUG
            Debug.Assert(id < this.View.Items.Count);
#endif
            var itemLayer = this.GetItemLayer(this.View.Items[id]);
            var itemLayout = itemLayer.ItemLayout;

            return itemLayout.Row + itemLayout.BlockRow > this.ViewSettings.ViewRowDisplay + this.RowOffset;
        }


        #endregion

        #region Operations

        public override void MoveDown()
        {
            // Items is empty
            if (this.View.Items.Count == 0)
            {
                return;
            }

            if (this.CanMoveDown)
            {
                var itemLayer = this.GetItemLayer(this.View.Items[this.currentId]);
                var itemLayout = itemLayer.ItemLayout;

                this.RowOffset += itemLayout.BlockRow;

                if (this.currentId < this.View.Items.Count - 1)
                {
                    ++this.currentId;
                }
            }
        }

        public override void MoveUp()
        {
            // Items is empty
            if (this.View.Items.Count == 0)
            {
                return;
            }

            if (this.CanMoveUp)
            {
                if (this.currentId > 0)
                {
                    --this.currentId;
                }

                var itemLayer = this.GetItemLayer(this.View.Items[this.currentId]);
                var itemLayout = itemLayer.ItemLayout;

                this.RowOffset -= itemLayout.BlockRow;
            }
        }

        #endregion
    }
}