namespace MetaMind.Engine.Guis.Widgets.Views.Selections
{
    using Layers;
    using Scrolls;

    public class PointViewVerticalSelectionController : ViewComponent, IPointViewVerticalSelectionController
    {
        private IPointViewVerticalScrollController viewScroll;

        private int? currentId;

        private int? previousId;

        #region Constructors

        public PointViewVerticalSelectionController(IView view)
            : base(view)
        {
        }

        #endregion

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;
        }

        #endregion

        public bool HasPreviouslySelected
        {
            get { return this.previousId != null; }
        }

        public bool HasSelected
        {
            get { return this.currentId != null; }
        }

        public int? PreviousSelectedId
        {
            get { return this.previousId; }
        }

        public int? CurrentSelectedId
        {
            get { return this.currentId; }
        }

        public void Cancel()
        {
            this.previousId = this.currentId;
            this.currentId = null;

            this.View[ViewState.View_Has_Selection] = () => false;
        }

        public bool IsSelected(int id)
        {
            return this.currentId == id;
        }

        public virtual void MoveUp()
        {
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

            if (!this.currentId.HasValue)
            {
                this.Reverse();

                return;
            }

            var id = this.currentId.Value;

            if (!this.IsTopmost(id))
            {
                this.Select(this.PreviousRow(id));
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsUpToDisplay(this.PreviousRow(id)))
            {
                this.viewScroll.MoveUp();
            }
        }

        public virtual void MoveDown()
        {
            if (this.View.ItemsRead.Count == 0)
            {
                return;
            }

            if (!this.currentId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id = this.currentId.Value;

            if (!this.IsBottommost(id))
            {
                this.Select(this.NextRow(id));
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsDownToDisplay(this.NextRow(id)))
            {
                this.viewScroll.MoveDown();
            }
        }

        public void Select(int id)
        {
            this.previousId = this.currentId;
            this.currentId = id;
        }

        protected virtual bool IsTopmost(int id)
        {
            var row = id;
            return row <= 0;
        }

        protected virtual bool IsBottommost(int id)
        {
            var row = id;
            return row >= this.View.ItemsRead.Count - 1;
        }

        protected void Reverse()
        {
            this.Select(this.previousId ?? 0);
        }

        private int NextRow(int id)
        {
            return id + 1;
        }

        private int PreviousRow(int id)
        {
            return id - 1;
        }
    }
}