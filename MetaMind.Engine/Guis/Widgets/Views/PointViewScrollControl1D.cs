﻿namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class PointViewScrollControl1D : ViewComponent, IPointViewScrollControlHorizontal
    {
        public PointViewScrollControl1D(IView view, PointViewSettings1D viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public int XOffset { get; private set; }

        private bool CanMoveLeft
        {
            get { return this.XOffset > 0; }
        }

        private bool CanMoveRight
        {
            get { return (this.ViewSettings.ColumnNumDisplay + this.XOffset) < this.View.Items.Count; }
        }

        public bool CanDisplay(int id)
        {
            var column = id;
            return this.XOffset <= column && column < this.ViewSettings.ColumnNumDisplay + this.XOffset;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.XOffset - 1;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.ViewSettings.ColumnNumDisplay + this.XOffset;
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                --this.XOffset;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                ++this.XOffset;
            }
        }

        public Point RootCenterPoint(int id)
        {
            return new Point(
                this.ViewSettings.Direction == PointViewDirection.Normal ?
                this.ViewSettings.PointStart.X - (this.XOffset * this.ViewSettings.PointMargin.X) + id * this.ViewSettings.PointMargin.X :
                this.ViewSettings.PointStart.X + (this.XOffset * this.ViewSettings.PointMargin.X) - id * this.ViewSettings.PointMargin.X,
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