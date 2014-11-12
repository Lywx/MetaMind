﻿namespace MetaMind.Engine.Guis.Elements.Views
{
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

    public class ViewScrollControl1D : ViewComponent, IViewScrollControlHorizontal
    {
        private int scroll;

        public ViewScrollControl1D(IView view, ViewSettings1D viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public int XOffset { get { return this.scroll; } }

        private bool CanMoveLeft
        {
            get { return this.scroll > 0; }
        }

        private bool CanMoveRight
        {
            get { return (ViewSettings.ColumnNumDisplay + this.scroll) < View.Items.Count; }
        }

        public bool CanDisplay(int id)
        {
            return this.scroll <= id && id < ViewSettings.ColumnNumDisplay + this.scroll;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.scroll - 1;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > ViewSettings.ColumnNumDisplay + this.scroll;
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                --this.scroll;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                ++this.scroll;
            }
        }

        public Point RootCenterPoint(int id)
        {
            return new Point(
                ViewSettings.Direction == ViewSettings1D.ScrollDirection.Right ?
                ViewSettings.StartPoint.X - (this.scroll * ViewSettings.RootMargin.X) + id * ViewSettings.RootMargin.X :
                ViewSettings.StartPoint.X + (this.scroll * ViewSettings.RootMargin.X) - id * ViewSettings.RootMargin.X,
                ViewSettings.StartPoint.Y);
        }
    }
}