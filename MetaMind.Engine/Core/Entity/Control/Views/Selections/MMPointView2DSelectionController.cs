// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSelectionControl2D.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Core.Entity.Control.Views.Selections
{
    using Controllers;
    using Layers;
    using Layouts;
    using Scrolls;

    public class MMPointView2DSelectionController : MMViewControlComponent, IMMPointView2DSelectionController
    {
        private readonly MMPointView2DLayer viewLayer;

        private int? currentId;

        private int? previousId;

        public MMPointView2DSelectionController(IMMView view)
            : base(view)
        {
            this.viewLayer = this.GetViewLayer<MMPointView2DLayer>();
        }

        #region Indirect Dependency

        protected IMMPointView2DLayout ViewLayout
        {
            get { return this.ViewController.ViewLayout; }
        }

        protected IMMPointView2DScrollController ViewScroll
        {
            get { return this.viewLayer.ViewScroll; }
        }

        protected IMMPointView2DController ViewController
        {
            get { return this.viewLayer.ViewController; }
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

            this.View[MMViewState.View_Has_Selection] = () => false;
        }

        public bool IsSelected(int id)
        {
            return this.currentId == id;
        }

        public void MoveDown()
        {
            if (!this.currentId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id     = this.currentId.Value;
            var column = this.ViewLayout.ColumnOf(id);
            var row    = this.ViewLayout.RowOf(id);

            if (!this.IsBottommost(row))
            {
                row = row + 1;
                this.Select(row, column);
            }

            if (this.ViewScroll.IsDownToDisplay(row))
            {
                this.ViewScroll.MoveDown();
            }
        }

        public void MoveLeft()
        {
            if (!this.currentId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id     = this.currentId.Value;
            var column = this.ViewLayout.ColumnOf(id);
            var row    = this.ViewLayout.RowOf(id);

            if (!this.IsLeftmost(column))
            {
                column = column - 1;
                this.Select(row, column);
            }

            if (this.ViewScroll.IsLeftToDisplay(column))
            {
                this.ViewScroll.MoveLeft();
            }
        }

        public void MoveRight()
        {
            if (!this.currentId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id     = this.currentId.Value;
            var column = this.ViewLayout.ColumnOf(id);
            var row    = this.ViewLayout.RowOf(id);

            if (!this.IsRightmost(column))
            {
                column = column + 1;
                this.Select(row, column);
            }

            if (this.ViewScroll.IsRightToDisplay(column))
            {
                this.ViewScroll.MoveRight();
            }
        }

        public void MoveUp()
        {
            if (!this.currentId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id     = this.currentId.Value;
            var column = this.ViewLayout.ColumnOf(id);
            var row    = this.ViewLayout.RowOf(id);

            if (!this.IsTopmost(row))
            {
                row = row - 1;
                this.Select(row, column);
            }

            if (this.ViewScroll.IsUpToDisplay(row))
            {
                this.ViewScroll.MoveUp();
            }
        }

        public void Select(int id)
        {
            this.previousId = this.currentId;
            this.currentId = id;
        }

        private bool IsBottommost(int row)
        {
            return row >= this.ViewLayout.RowNum - 1;
        }

        private bool IsLeftmost(int column)
        {
            return column <= 0;
        }

        private bool IsRightmost(int column)
        {
            return column >= this.ViewLayout.ColumnNum - 1;
        }

        private bool IsTopmost(int row)
        {
            return row <= 0;
        }

        private void Reverse()
        {
            this.Select(this.previousId.HasValue ? this.previousId.Value : 0);
        }

        private void Select(int row, int column)
        {
            this.previousId = this.currentId;
            this.currentId = this.ViewLayout.IdFrom(row, column);
        }
    }
}