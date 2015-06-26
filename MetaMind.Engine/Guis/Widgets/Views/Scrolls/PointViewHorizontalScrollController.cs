﻿namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    using Microsoft.Xna.Framework;

    public class PointViewHorizontalScrollController : ViewComponent, IPointViewHorizontalScrollController
    {
        private readonly PointViewHorizontalSettings viewSettings;

        public PointViewHorizontalScrollController(IView view)
            : base(view)
        {
            this.viewSettings = this.ViewGetLayer<PointViewHorizontalLayer>().ViewSettings;
        }

        public int ColumnOffset { get; private set; }

        private bool CanMoveLeft
        {
            get
            {
                return this.ColumnOffset > 0;
            }
        }

        private bool CanMoveRight
        {
            get
            {
                return (this.viewSettings.ViewColumnDisplay + this.ColumnOffset) < this.View.ItemsRead.Count;
            }
        }

        public bool CanDisplay(int id)
        {
            var column = id;
            return this.ColumnOffset <= column && column < this.viewSettings.ViewColumnDisplay + this.ColumnOffset;
        }

        public bool IsLeftToDisplay(int id)
        {
            var column = id;
            return column < this.ColumnOffset - 1;
        }

        public bool IsRightToDisplay(int id)
        {
            var column = id;
            return column > this.viewSettings.ViewColumnDisplay + this.ColumnOffset;
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                --this.ColumnOffset;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                ++this.ColumnOffset;
            }
        }

        public Vector2 Position(int id)
        {
            var column = id;
            return new Vector2(
                this.viewSettings.ViewDirection == ViewDirection.Normal
                    ? this.viewSettings.ViewPosition.X - (this.ColumnOffset * this.viewSettings.ItemMargin.X) + column * this.viewSettings.ItemMargin.X
                    : this.viewSettings.ViewPosition.X + (this.ColumnOffset * this.viewSettings.ItemMargin.X) - column * this.viewSettings.ItemMargin.X,
                this.viewSettings.ViewPosition.Y);
        }

        public void Zoom(int id)
        {
            if (!this.CanDisplay(id))
            {
                while (this.IsLeftToDisplay(id))
                {
                    this.MoveLeft();
                }

                while (this.IsRightToDisplay(id))
                {
                    this.MoveRight();
                }
            }
        }

        public void Reset()
        {
            this.ColumnOffset = 0;
        }
    }
}