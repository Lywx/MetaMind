// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSelectionControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public interface IPointViewSelectionControl2D : IPointViewSelectionControl1D
    {
        void MoveDown();

        void MoveUp();
    }

    public class PointViewSelectionControl2D : ViewComponent, IPointViewSelectionControl2D
    {
        private int? currentId;

        private int? previousId;

        public PointViewSelectionControl2D(IView view, PointViewSettings2D viewSettings, ItemSettings itemSettings)
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

            this.View.Disable(ViewState.View_Has_Selection);
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
            var column = this.ViewControl.ColumnFrom(id);
            var row    = this.ViewControl.RowFrom(id);

            if (!this.IsBottommost(row))
            {
                row = row + 1;
                this.Select(row, column);
            }

            if (this.ViewControl.Scroll.IsDownToDisplay(row))
            {
                this.ViewControl.Scroll.MoveDown();
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
            var column = this.ViewControl.ColumnFrom(id);
            var row    = this.ViewControl.RowFrom(id);

            if (!this.IsLeftmost(column))
            {
                column = column - 1;
                this.Select(row, column);
            }

            if (this.ViewControl.Scroll.IsLeftToDisplay(column))
            {
                this.ViewControl.Scroll.MoveLeft();
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
            var column = this.ViewControl.ColumnFrom(id);
            var row    = this.ViewControl.RowFrom(id);

            if (!this.IsRightmost(column))
            {
                column = column + 1;
                this.Select(row, column);
            }

            if (this.ViewControl.Scroll.IsRightToDisplay(column))
            {
                this.ViewControl.Scroll.MoveRight();
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
            var column = this.ViewControl.ColumnFrom(id);
            var row    = this.ViewControl.RowFrom(id);

            if (!this.IsTopmost(row))
            {
                row = row - 1;
                this.Select(row, column);
            }

            if (this.ViewControl.Scroll.IsUpToDisplay(row))
            {
                this.ViewControl.Scroll.MoveUp();
            }
        }

        public void Select(int id)
        {
            this.previousId = this.currentId;
            this.currentId = id;
        }

        private bool IsBottommost(int row)
        {
            return row >= this.ViewControl.RowNum - 1;
        }

        private bool IsLeftmost(int column)
        {
            return column <= 0;
        }

        private bool IsRightmost(int column)
        {
            return column >= this.ViewControl.ColumnNum - 1;
        }

        private bool IsTopmost(int row)
        {
            return row <= 0;
        }

        private void Select(int row, int column)
        {
            this.previousId = this.currentId;
            this.currentId = this.ViewControl.IdFrom(row, column);
        }

        private void Reverse()
        {
            this.Select(this.previousId.HasValue ? this.previousId.Value : 0);
        }
    }
}