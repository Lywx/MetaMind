namespace MetaMind.Engine.Guis.Widgets.Views.Selections
{
    using System.Diagnostics;
    using Items.Layers;
    using Items.Layouts;
    using Scrolls;

    public class BlockViewVerticalSelectionController : PointViewVerticalSelectionController, IBlockViewVerticalSelectionController 
    {
        private IBlockViewVerticalScrollController viewScroll;

        public BlockViewVerticalSelectionController(IView view) : base(view)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<BlockViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;
        }

        public override void MoveUp()
        {
            if (!this.CurrentSelectedId.HasValue)
            {
                this.Reverse();

                return;
            }

            var id = this.CurrentSelectedId.Value;

            if (!this.IsTopmost(id))
            {
                this.Select(this.PreviousId(id));
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsUpToDisplay(this.PreviousId(id)))
            {
                this.viewScroll.MoveUp();
            }
        }

        public override void MoveDown()
        {
            if (!this.CurrentSelectedId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id = this.CurrentSelectedId.Value;

            if (!this.IsBottommost(id))
            {
                this.Select(this.NextId(id));
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsDownToDisplay(this.NextId(id)))
            {
                this.viewScroll.MoveDown();
            }
        }

        protected override bool IsTopmost(int id)
        {
#if DEBUG
            Debug.Assert(id >= 0);
#endif
            var itemLayer = this.ItemsRead[id].GetLayer<BlockViewVerticalItemLayer>();
            return itemLayer.ItemLayout.Row <= 0;
        }

        protected override bool IsBottommost(int id)
        {
#if DEBUG
            Debug.Assert(id < this.View.ItemsRead.Count);
#endif
            return id == this.View.ItemsRead.Count - 1;
        }

        private int NextId(int id)
        {
            return id < this.View.ItemsRead.Count - 1 ? id + 1 : this.View.ItemsRead.Count - 1;
        }

        private int PreviousId(int id)
        {
            return id > 0 ? id - 1 : id;
        }
    }
}