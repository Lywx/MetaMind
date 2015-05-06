// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewSelectionControl2D.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Views.Selections
{
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;

    public class PointView2DSelectionControl : ViewComponent, IPointView2DSelectionControl
    {
        private readonly IPointView2DLogic viewLogic; 

        private readonly IPointView2DScrollControl viewScroll;

        private readonly IPointView2DLayout viewLayout;

        private int? currentId;

        private int? previousId;

        public PointView2DSelectionControl(IView view)
            : base(view)
        {
            var viewLayer = this.ViewGetLayer<PointView2DLayer>();
            this.viewLogic = viewLayer.ViewLogic;
            this.viewScroll = viewLayer.ViewScroll;
            this.viewLayout = viewLogic.ViewLayout;
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

        public void MoveDown()
        {
            if (!this.currentId.HasValue)
            {
                this.Reverse();
                return;
            }

            var id     = this.currentId.Value;
            var column = this.viewLayout.ColumnFrom(id);
            var row    = this.viewLayout.RowFrom(id);

            if (!this.IsBottommost(row))
            {
                row = row + 1;
                this.Select(row, column);
            }

            if (this.viewScroll.IsDownToDisplay(row))
            {
                this.viewScroll.MoveDown();
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
            var column = this.viewLayout.ColumnFrom(id);
            var row    = this.viewLayout.RowFrom(id);

            if (!this.IsLeftmost(column))
            {
                column = column - 1;
                this.Select(row, column);
            }

            if (this.viewScroll.IsLeftToDisplay(column))
            {
                this.viewScroll.MoveLeft();
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
            var column = this.viewLayout.ColumnFrom(id);
            var row    = this.viewLayout.RowFrom(id);

            if (!this.IsRightmost(column))
            {
                column = column + 1;
                this.Select(row, column);
            }

            if (this.viewScroll.IsRightToDisplay(column))
            {
                this.viewScroll.MoveRight();
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
            var column = this.viewLayout.ColumnFrom(id);
            var row    = this.viewLayout.RowFrom(id);

            if (!this.IsTopmost(row))
            {
                row = row - 1;
                this.Select(row, column);
            }

            if (this.viewScroll.IsUpToDisplay(row))
            {
                this.viewScroll.MoveUp();
            }
        }

        public void Select(int id)
        {
            this.previousId = this.currentId;
            this.currentId = id;
        }

        private bool IsBottommost(int row)
        {
            return row >= this.viewLayout.RowNum - 1;
        }

        private bool IsLeftmost(int column)
        {
            return column <= 0;
        }

        private bool IsRightmost(int column)
        {
            return column >= this.viewLayout.ColumnNum - 1;
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
            this.currentId = this.viewLayout.IdFrom(row, column);
        }
    }
}