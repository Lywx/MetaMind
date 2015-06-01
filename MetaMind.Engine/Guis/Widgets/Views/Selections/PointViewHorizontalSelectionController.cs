// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewHorizontalSelectionController.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Selections
{
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;

    public class PointViewHorizontalSelectionController : ViewComponent, IPointViewHorizontalSelectionController
    {
        private readonly IPointViewHorizontalScrollController viewScroll;

        private int? currentColumn;

        private int? previousColumn;

        #region Constructors

        public PointViewHorizontalSelectionController(IView view)
            : base(view)
        {
            var viewLayer = this.ViewGetLayer<PointViewHorizontalLayer>();
            this.viewScroll = viewLayer.ViewScroll;
        }

        #endregion

        public bool HasPreviouslySelected
        {
            get { return this.previousColumn != null; }
        }

        public bool HasSelected
        {
            get { return this.currentColumn != null; }
        }

        public int? PreviousSelectedId
        {
            get { return this.previousColumn; }
        }

        public int? SelectedId
        {
            get { return this.currentColumn; }
        }

        public void Cancel()
        {
            this.previousColumn = this.currentColumn;
            this.currentColumn = null;

            this.View[ViewState.View_Has_Selection] = () => false;
        }

        public bool IsSelected(int id)
        {
            return this.currentColumn == id;
        }

        public void MoveLeft()
        {
            if (!this.currentColumn.HasValue)
            {
                this.Reverse();

                return;
            }

            var column = this.currentColumn.Value;

            if (!this.IsLeftmost(column))
            {
                column = column - 1;
                this.Select(column);
            }

            if (this.viewScroll != null && 
                this.viewScroll.IsLeftToDisplay(column))
            {
                this.viewScroll.MoveLeft();
            }
        }

        public void MoveRight()
        {
            if (!this.currentColumn.HasValue)
            {
                this.Reverse();
                return;
            }

            var column = this.currentColumn.Value;

            if (!this.IsRightmost(column))
            {
                column = column + 1;
                this.Select(column);
            }

            if (this.viewScroll != null && this.viewScroll.IsRightToDisplay(column))
            {
                this.viewScroll.MoveRight();
            }
        }

        public void Select(int id)
        {
            this.previousColumn = this.currentColumn;
            this.currentColumn = id;
        }

        private bool IsLeftmost(int column)
        {
            return column <= 0;
        }

        private bool IsRightmost(int column)
        {
            return column >= this.View.Items.Count - 1;
        }

        private void Reverse()
        {
            this.Select(this.previousColumn.HasValue ? this.previousColumn.Value : 0);
        }
    }
}