// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSelectionControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;

    public class PointView2DSelectionControl : ViewComponent, IPointView2DSelectionControl
    {
        private int? currentId;

        private int? previousId;

        public PointView2DSelectionControl(IView view, ICloneable viewSettings, ICloneable itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

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

        public int? SelectedId
        {
            get { return this.currentId; }
        }

        public void Clear()
        {
            this.previousId = this.currentId;
            this.currentId = null;

            this.View[ViewState.View_Has_Selection] = () => false;
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
            var column = this.ViewLogic.ColumnFrom(id);
            var row    = this.ViewLogic.RowFrom(id);

            if (!this.IsBottommost(row))
            {
                row = row + 1;
                this.Select(row, column);
            }

            if (this.ViewLogic.Scroll.IsDownToDisplay(row))
            {
                this.ViewLogic.Scroll.MoveDown();
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
            var column = this.ViewLogic.ColumnFrom(id);
            var row    = this.ViewLogic.RowFrom(id);

            if (!this.IsLeftmost(column))
            {
                column = column - 1;
                this.Select(row, column);
            }

            if (this.ViewLogic.Scroll.IsLeftToDisplay(column))
            {
                this.ViewLogic.Scroll.MoveLeft();
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
            var column = this.ViewLogic.ColumnFrom(id);
            var row    = this.ViewLogic.RowFrom(id);

            if (!this.IsRightmost(column))
            {
                column = column + 1;
                this.Select(row, column);
            }

            if (this.ViewLogic.Scroll.IsRightToDisplay(column))
            {
                this.ViewLogic.Scroll.MoveRight();
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
            var column = this.ViewLogic.ColumnFrom(id);
            var row    = this.ViewLogic.RowFrom(id);

            if (!this.IsTopmost(row))
            {
                row = row - 1;
                this.Select(row, column);
            }

            if (this.ViewLogic.Scroll.IsUpToDisplay(row))
            {
                this.ViewLogic.Scroll.MoveUp();
            }
        }

        public void Select(int id)
        {
            this.previousId = this.currentId;
            this.currentId = id;
        }

        private bool IsBottommost(int row)
        {
            return row >= this.ViewLogic.RowNum - 1;
        }

        private bool IsLeftmost(int column)
        {
            return column <= 0;
        }

        private bool IsRightmost(int column)
        {
            return column >= this.ViewLogic.ColumnNum - 1;
        }

        private bool IsTopmost(int row)
        {
            return row <= 0;
        }

        private void Select(int row, int column)
        {
            this.previousId = this.currentId;
            this.currentId = this.ViewLogic.IdFrom(row, column);
        }

        private void Reverse()
        {
            this.Select(this.previousId.HasValue ? this.previousId.Value : 0);
        }
    }
}