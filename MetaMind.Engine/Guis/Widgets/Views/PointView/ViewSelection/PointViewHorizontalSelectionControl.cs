// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointViewHorizontalSelectionControl.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewSelection
{
    using MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll;

    public class PointViewHorizontalSelectionControl : ViewComponent, IPointViewHorizontalSelectionControl
    {
        private int? currentColumn;

        private int? previousColumn;

        #region Constructors

        public PointViewHorizontalSelectionControl(IView view)
            : base(view)
        {
        }

        #endregion

        #region Dependency

        protected IViewScrollSupport ViewScrollSupport
        {
            get
            {
                return this.ViewLogic as IViewScrollSupport;
            }
        }

        protected IPointViewHorizontalScrollControl ViewScroll
        {
            get
            {
                // Two layer certification
                return this.ViewScrollSupport != null
                           ? this.ViewScrollSupport.ViewScroll as IPointViewHorizontalScrollControl
                           : null;
            }
        }

        #endregion Dependency

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

        public void Clear()
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

            if (this.ViewScroll != null && 
                this.ViewScroll.IsLeftToDisplay(column))
            {
                this.ViewScroll.MoveLeft();
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

            if (this.ViewScroll != null && this.ViewScroll.IsRightToDisplay(column))
            {
                this.ViewScroll.MoveRight();
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