// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewHorizontalSelectionController.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Controls.Views.Selections
{
    using Layers;
    using Scrolls;

    public class PointViewHorizontalSelectionController : ViewComponent, IPointViewHorizontalSelectionController
    {
        private IPointViewHorizontalScrollController viewScroll;

        private int? currentId;

        private int? previousId;

        #region Constructors

        public PointViewHorizontalSelectionController(IMMViewNode view)
            : base(view)
        {
        }

        public override void Initialize()
        {
            var viewLayer = this.GetViewLayer<PointViewHorizontalLayer>();
            this.viewScroll = viewLayer.ViewScroll;

            base.Initialize();
        }

        #endregion

        public bool HasPreviouslySelected => this.previousId != null;

        public bool HasSelected => this.currentId != null;

        public int? PreviousSelectedId => this.previousId;

        public int? CurrentSelectedId => this.currentId;

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

        public void MoveLeft()
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

            var column = this.currentId.Value;

            if (!this.IsLeftmost(column))
            {
                this.Select(this.PreviousColumn(column));
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsLeftToDisplay(this.PreviousColumn(column)))
            {
                this.viewScroll.MoveLeft();
            }
        }

        public void MoveRight()
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

            var column = this.currentId.Value;

            if (!this.IsRightmost(column))
            {
                this.Select(this.NextColumn(column));
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsRightToDisplay(this.NextColumn(column)))
            {
                this.viewScroll.MoveRight();
            }
        }

        public void Select(int id)
        {
            this.previousId = this.currentId;
            this.currentId = id;
        }

        private bool IsLeftmost(int column)
        {
            return column <= 0;
        }

        private bool IsRightmost(int column)
        {
            return column >= this.View.ItemsRead.Count - 1;
        }

        private void Reverse()
        {
            this.Select(this.previousId ?? 0);
        }

        private int NextColumn(int column)
        {
            return column + 1;
        }

        private int PreviousColumn(int column)
        {
            return column - 1;
        }

    }
}