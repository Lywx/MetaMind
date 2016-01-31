namespace MetaMind.Engine.Core.Entity.Control.Views.Selections
{
    using System.Diagnostics;
    using Item.Layers;
    using Scrolls;

    public class MMBlockViewVerticalSelectionController : MMPointViewVerticalSelectionController, IMMBlockViewVerticalSelectionController 
    {
        private IMMBlockViewVerticalScrollController viewScroll;

        public MMBlockViewVerticalSelectionController(IMMView view) : base(view)
        {
        }

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            var viewLayer = this.GetViewLayer<MMBlockViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;
        }

        #endregion

        #region State

        protected override bool IsBottommost(int id)
        {
#if DEBUG
            Debug.Assert(
                this.View.Items.Count == 0 || id < this.View.Items.Count);
#endif
            return id == this.View.Items.Count - 1;
        }

        protected int LowerId(int id)
        {
            return id < this.View.Items.Count - 1
                       ? id + 1
                       : this.View.Items.Count - 1;
        }

        protected int UpperId(int id)
        {
            return id > 0 ? id - 1 : id;
        }

        #endregion

        #region Operations

        public override void MoveUp()
        {
            if (this.View.Items.Count == 0)
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
            if (this.View.Items.Count == 0)
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
            if (id >= this.View.Items.Count)
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