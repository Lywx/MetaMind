namespace MetaMind.Engine.Guis.Widgets.Views.Selections
{
    using Layers;
    using Scrolls;

    public class PointViewVerticalSelectionController : ViewComponent, IPointViewVerticalSelectionController
    {
        private IPointViewVerticalScrollController viewScroll;

        private int? currentRow;

        private int? previousRow;

        #region Constructors

        public PointViewVerticalSelectionController(IView view)
            : base(view)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;
        }

        #endregion

        public bool HasPreviouslySelected
        {
            get { return this.previousRow != null; }
        }

        public bool HasSelected
        {
            get { return this.currentRow != null; }
        }

        public int? PreviousSelectedId
        {
            get { return this.previousRow; }
        }

        public int? SelectedId
        {
            get { return this.currentRow; }
        }

        public void Cancel()
        {
            this.previousRow = this.currentRow;
            this.currentRow = null;

            this.View[ViewState.View_Has_Selection] = () => false;
        }

        public bool IsSelected(int id)
        {
            return this.currentRow == id;
        }

        public void MoveUp()
        {
            if (!this.currentRow.HasValue)
            {
                this.Reverse();

                return;
            }

            var row = this.currentRow.Value;

            if (!this.IsTopmost(row))
            {
                row = row - 1;
                this.Select(row);
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsUpToDisplay(row))
            {
                this.viewScroll.MoveUp();
            }
        }

        public void MoveDown()
        {
            if (!this.currentRow.HasValue)
            {
                this.Reverse();
                return;
            }

            var column = this.currentRow.Value;

            if (!this.IsBottommost(column))
            {
                column = column + 1;
                this.Select(column);
            }

            if (this.viewScroll != null && this.viewScroll.IsDownToDisplay(column))
            {
                this.viewScroll.MoveDown();
            }
        }

        public void Select(int id)
        {
            this.previousRow = this.currentRow;
            this.currentRow = id;
        }

        private bool IsTopmost(int row)
        {
            return row <= 0;
        }

        private bool IsBottommost(int row)
        {
            return row >= this.View.Items.Count - 1;
        }

        private void Reverse()
        {
            this.Select(this.previousRow ?? 0);
        }
    }
}