namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewScroll
{
    using System;

    using Microsoft.Xna.Framework;

    public class PointViewHorizontalScrollControl : ViewComponent, IPointViewHorizontalScrollControl
    {
        public PointViewHorizontalScrollControl(IView view, PointViewHorizontalSettings viewSettings)
            : base(view)
        {
            if (viewSettings == null)
            {
                throw new ArgumentNullException("viewSettings");
            }
        }

        public int OffsetX { get; private set; }

        private bool CanMoveLeft
        {
            get
            {
                return this.OffsetX > 0;
            }
        }

        private bool CanMoveRight
        {
            get
            {
                return (this.ViewSettings.ColumnNumDisplay + this.OffsetX) < this.View.Items.Count;
            }
        }

        #region Dependency

        /// <summary>
        /// Gets the dependency in a week form which is defined as a dynamic type in ViewComponent. 
        /// </summary>
        private new PointViewHorizontalSettings ViewSettings
        {
            get
            {
                return base.ViewSettings;
            }
        }

        #endregion

        public bool CanDisplay(int id)
        {
            var column = id;
            return this.OffsetX <= column && column < this.ViewSettings.ColumnNumDisplay + this.OffsetX;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.OffsetX - 1;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.ViewSettings.ColumnNumDisplay + this.OffsetX;
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                --this.OffsetX;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                ++this.OffsetX;
            }
        }

        public Point RootCenterPosition(int id)
        {
            return new Point(
                    this.ViewSettings.Direction == PointView1DDirection.Normal
                        ? this.ViewSettings.PointStart.X - (this.OffsetX * this.ViewSettings.PointMargin.X) + id * this.ViewSettings.PointMargin.X
                        : this.ViewSettings.PointStart.X + (this.OffsetX * this.ViewSettings.PointMargin.X) - id * this.ViewSettings.PointMargin.X,
                    this.ViewSettings.PointStart.Y);
        }

        public void Zoom(int id)
        {
            if (!this.CanDisplay(id))
            {
                var column = id;

                while (this.IsLeftToDisplay(column))
                {
                    this.MoveLeft();
                }

                while (this.IsRightToDisplay(column))
                {
                    this.MoveRight();
                }
            }
        }
    }
}