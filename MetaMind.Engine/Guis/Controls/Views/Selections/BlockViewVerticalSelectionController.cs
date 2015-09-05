namespace MetaMind.Engine.Guis.Controls.Views.Selections
{
    using System.Diagnostics;
    using Items.Layers;
    using Scrolls;

    public class BlockViewVerticalSelectionController : PointViewVerticalSelectionController, IBlockViewVerticalSelectionController 
    {
        private IBlockViewVerticalScrollController viewScroll;

        public BlockViewVerticalSelectionController(IView view) : base(view)
        {
        }

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<BlockViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;
        }

        #endregion

        #region State

        protected override bool IsBottommost(int id)
        {
#if DEBUG
            Debug.Assert(
                this.View.ItemsRead.Count == 0 || id < this.View.ItemsRead.Count);
#endif
            return id == this.View.ItemsRead.Count - 1;
        }

        protected int LowerId(int id)
        {
            return id < this.View.ItemsRead.Count - 1
                       ? id + 1
                       : this.View.ItemsRead.Count - 1;
        }

        protected int UpperId(int id)
        {
            return id > 0 ? id - 1 : id;
        }

        #endregion

        #region Operations

        public override void MoveUp()
        {
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

            if (!this.CurrentSelectedId.HasValue)
            {
                this.Reverse();

                return;
            }

            var id = this.CurrentSelectedId.Value;

            if (!this.IsTopmost(id))
            {
                this.Select(this.UpperId(id));
            }

            if (this.viewScroll != null &&
                this.viewScroll.IsUpToDisplay(this.UpperId(id)))
            {
                this.viewScroll.MoveUp();
            }
        }

        public override void MoveDown()
        {
            // Items is empty
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

            if (!this.CurrentSelectedId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id = this.CurrentSelectedId.Value;

            // Last item is deleted
            if (id >= this.View.ItemsRead.Count)
            {
                return;
            }

            if (!this.IsBottommost(id))
            {
                this.Select(this.LowerId(id));
            }

            if (this.viewScroll != null &&
                this.viewScroll.IsDownToDisplay(this.LowerId(id)))
            {
                this.viewScroll.MoveDown();
            }
        }

        #endregion
    }
}