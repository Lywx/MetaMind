// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointView1DSelectionControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewSelection
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class PointView1DSelectionControl : ViewComponent, IPointView1DSelectionControl
    {
        private int? currentColumn;

        private int? previousColumn;

        public PointView1DSelectionControl(IView view, PointViewHorizontalSettings viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

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

            if (this.ViewLogic.Scroll.IsLeftToDisplay(column))
            {
                this.ViewLogic.Scroll.MoveLeft();
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

            if (this.ViewLogic.Scroll.IsRightToDisplay(column))
            {
                this.ViewLogic.Scroll.MoveRight();
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