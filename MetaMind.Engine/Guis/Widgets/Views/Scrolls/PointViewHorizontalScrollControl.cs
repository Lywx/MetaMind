﻿namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.PointView;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    using Microsoft.Xna.Framework;

    public class PointViewHorizontalScrollControl : ViewComponent, IPointViewHorizontalScrollControl
    {
        private readonly PointViewHorizontalSettings viewSettings;

        public PointViewHorizontalScrollControl(IView view)
            : base(view)
        {
            this.viewSettings = this.ViewGetLayer<PointViewHorizontalLayer>().ViewSettings;
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
                return (this.viewSettings.ColumnNumDisplay + this.OffsetX) < this.View.Items.Count;
            }
        }

        public bool CanDisplay(int id)
        {
            var column = id;
            return this.OffsetX <= column && column < this.viewSettings.ColumnNumDisplay + this.OffsetX;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.OffsetX - 1;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.viewSettings.ColumnNumDisplay + this.OffsetX;
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

        public Vector2 Position(int id)
        {
            return new Vector2(
                this.viewSettings.Direction == PointViewDirection.Normal
                    ? this.viewSettings.PointStart.X - (this.OffsetX * this.viewSettings.PointMargin.X) + id * this.viewSettings.PointMargin.X
                    : this.viewSettings.PointStart.X + (this.OffsetX * this.viewSettings.PointMargin.X) - id * this.viewSettings.PointMargin.X,
                this.viewSettings.PointStart.Y);
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