namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// Dependent on PointView2DSettings IPointView2DLogic
    /// </summary>
    /// <see cref="PointView2DSettings"/>
    /// <see cref="IPointView2DLogic"/>
    public class PointView2DScrollControl : ViewComponent, IPointView2DScrollControl
    {
        private int offsetX;

        private int offsetY;

        public PointView2DScrollControl(IView view, PointView2DSettings viewSettings)
            : base(view)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException("viewSettings");
            }

            this.ViewSettings = viewSettings;
        }

        #region Dependency

        private new PointView2DSettings ViewSettings { get; set; }

        private int ColumnFrom(int id)
        {
            return ((IPointView2DLogic)this.ViewLogic).ColumnFrom(id);
        }

        private int RowFrom(int id)
        {
            return ((IPointView2DLogic)this.ViewLogic).RowFrom(id);
        }

        #endregion

        public int OffsetX
        {
            get { return this.offsetX; }
        }

        public int OffsetY
        {
            get { return this.offsetY; }
        }

        private bool CanMoveDown
        {
            get
            {
                return (this.ViewSettings.ColumnNumDisplay * (this.ViewSettings.RowNumDisplay + this.OffsetY) < this.ViewSettings.ColumnNumDisplay * this.ViewSettings.RowNumMax) && 
                       (this.ViewSettings.ColumnNumMax * (this.ViewSettings.RowNumDisplay + this.OffsetY) < this.View.ViewItems.Count);
            }
        }

        private bool CanMoveLeft
        {
            get { return this.OffsetX > 0; }
        }

        private bool CanMoveRight
        {
            get
            {
                return this.ViewSettings.RowNumDisplay * (this.ViewSettings.ColumnNumDisplay + this.OffsetX)
                       < this.ViewSettings.RowNumDisplay * this.ViewSettings.ColumnNumMax;
            }
        }

        private bool CanMoveUp
        {
            get { return this.OffsetY > 0; }
        }

        public bool CanDisplay(int row, int column)
        {
            return this.OffsetX <= column && column < this.ViewSettings.ColumnNumDisplay + this.OffsetX &&
                   this.OffsetY <= row    && row    < this.ViewSettings.RowNumDisplay    + this.OffsetY;
        }

        public bool CanDisplay(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);

            return this.CanDisplay(row, column);
        }

        public bool IsDownToDisplay(int row)
        {
            return row > this.ViewSettings.RowNumDisplay + this.OffsetY - 1;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.OffsetX;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.ViewSettings.ColumnNumDisplay + this.OffsetX - 1;
        }

        public bool IsUpToDisplay(int row)
        {
            return row < this.OffsetY;
        }

        public void MoveDown()
        {
            if (this.CanMoveDown)
            {
                this.offsetY = this.OffsetY + 1;
            }
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                this.offsetX = this.OffsetX - 1;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                this.offsetX = this.OffsetX + 1;
            }
        }

        public void MoveUp()
        {
            if (this.CanMoveUp)
            {
                this.offsetY = this.OffsetY - 1;
            }
        }

        public void MoveUpToTop()
        {
            this.offsetY = 0;
        }

        public Point RootCenterPosition(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);
            return new Point(
                this.ViewSettings.PointStart.X - this.OffsetX * this.ViewSettings.PointMargin.X + column * this.ViewSettings.PointMargin.X,
                this.ViewSettings.PointStart.Y - this.OffsetY * this.ViewSettings.PointMargin.Y + row * this.ViewSettings.PointMargin.Y);
        }

        public void Zoom(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);

            if (!this.CanDisplay(row, column))
            {
                while (this.IsLeftToDisplay(column))
                {
                    this.MoveLeft();
                }

                while (this.IsRightToDisplay(column))
                {
                    this.MoveRight();
                }

                while (this.IsUpToDisplay(row))
                {
                    this.MoveUp();
                }

                while (this.IsDownToDisplay(row))
                {
                    this.MoveDown();
                }
            }
        }
    }
}