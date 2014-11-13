// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSelectionControl2D.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements.Views
{
    using MetaMind.Engine.Guis.Elements.Items;

    public interface IViewSelectionControl2D : IViewSelectionControl1D
    {
        void MoveDown();

        void MoveUp();
    }

    public class ViewSelectionControl2D : ViewComponent, IViewSelectionControl2D
    {
        private int? currentId;

        private int? previousId;

        public ViewSelectionControl2D(IView view, ViewSettings2D viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public bool HasSelected
        {
            get { return this.currentId != null; }
        }

        public int? SelectedId
        {
            get { return currentId; }
        }

        public void Clear()
        {
            this.previousId = this.currentId;
            this.currentId = null;

            // TODO: may not be a good design
            View.Disable(ViewState.View_Has_Selection);

            if (View.Items.Count != 0 &&
                this.previousId.HasValue)
            {
                View.Items[this.previousId.Value].Disable(ItemState.Item_Selected);
            }
        }

        public bool IsSelected(int id)
        {
            return this.currentId == id;
        }

        public void MoveDown()
        {
            if (!this.currentId.HasValue)
            {
                this.SelectInit();
            }
            else if (!this.IsBottommost(this.ViewControl.RowFrom(this.currentId.Value)))
            {
                var row = this.ViewControl.RowFrom(this.currentId.Value) + 1;
                var column = this.ViewControl.ColumnFrom(this.currentId.Value);
                this.Select(row, column);
                if (this.ViewControl.Scroll.IsDownToDisplay(row))
                {
                    this.ViewControl.Scroll.MoveDown();
                }
            }
        }

        public void MoveLeft()
        {
            if (!this.currentId.HasValue)
            {
                this.SelectInit();
            }
            else if (!this.IsLeftmost(this.ViewControl.ColumnFrom(this.currentId.Value)))
            {
                var row = this.ViewControl.RowFrom(this.currentId.Value);
                var column = this.ViewControl.ColumnFrom(this.currentId.Value) - 1;
                this.Select(row, column);
                if (this.ViewControl.Scroll.IsLeftToDisplay(column))
                {
                    this.ViewControl.Scroll.MoveLeft();
                }
            }
        }

        public void MoveRight()
        {
            if (!this.currentId.HasValue)
            {
                this.SelectInit();
            }
            else if (!this.IsRightmost(this.ViewControl.ColumnFrom(this.currentId.Value)))
            {
                var row = this.ViewControl.RowFrom(this.currentId.Value);
                var column = this.ViewControl.ColumnFrom(this.currentId.Value) + 1;
                this.Select(row, column);
                if (this.ViewControl.Scroll.IsRightToDisplay(column))
                {
                    this.ViewControl.Scroll.MoveRight();
                }
            }
        }

        public void MoveUp()
        {
            if (!this.currentId.HasValue)
            {
                this.SelectInit();
            }
            else if (!this.IsTopmost(this.ViewControl.RowFrom(this.currentId.Value)))
            {
                var row = this.ViewControl.RowFrom(this.currentId.Value) - 1;
                var column = this.ViewControl.ColumnFrom(this.currentId.Value);
                this.Select(row, column);
                if (this.ViewControl.Scroll.IsUpToDisplay(row))
                {
                    this.ViewControl.Scroll.MoveUp();
                }
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

        private void SelectInit()
        {
            this.Select(this.previousId.HasValue ? this.previousId.Value : 0);
        }
    }
}