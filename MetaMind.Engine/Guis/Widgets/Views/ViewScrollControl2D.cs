namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class ViewScrollControl2D : ViewComponent, IViewScrollControlHorizontal, IViewScrollControlVertical
    {
        private int xOffset;
        private int yOffset;

        public ViewScrollControl2D(IView view, ViewSettings2D viewSettings, ItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
        }

        public int XOffset
        {
            get { return this.xOffset; }
        }

        public int YOffset
        {
            get { return this.yOffset; }
        }

        private bool CanMoveDown
        {
            get
            {
                return (this.ViewSettings.ColumnNumDisplay * (this.ViewSettings.RowNumDisplay + this.YOffset) < this.ViewSettings.ColumnNumDisplay * this.ViewSettings.RowNumMax) &&
                       (this.ViewSettings.ColumnNumMax     * (this.ViewSettings.RowNumDisplay + this.YOffset) < this.View.Items.Count);
            }
        }

        private bool CanMoveLeft
        {
            get { return this.XOffset > 0; }
        }

        private bool CanMoveRight
        {
            get
            {
                return this.ViewSettings.RowNumDisplay * (this.ViewSettings.ColumnNumDisplay + this.XOffset) < this.ViewSettings.RowNumDisplay * this.ViewSettings.ColumnNumMax;
            }
        }

        private bool CanMoveUp
        {
            get { return this.YOffset > 0; }
        }

        public bool CanDisplay(int id)
        {
            var row = this.ViewControl.RowFrom(id);
            var column = this.ViewControl.ColumnFrom(id);
            return this.XOffset <= column && column < this.ViewSettings.ColumnNumDisplay + this.XOffset &&
                   this.YOffset <= row    && row    < this.ViewSettings.RowNumDisplay    + this.YOffset;
        }

        public bool IsDownToDisplay(int row)
        {
            return row > this.ViewSettings.RowNumDisplay + this.YOffset - 1;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.XOffset;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.ViewSettings.ColumnNumDisplay + this.XOffset - 1;
        }

        public bool IsUpToDisplay(int row)
        {
            return row < this.YOffset;
        }

        public void MoveDown()
        {
            if (this.CanMoveDown)
            {
                this.yOffset = this.YOffset + 1;
            }
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                this.xOffset = this.XOffset - 1;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                this.xOffset = this.XOffset + 1;
            }
        }

        public void MoveUp()
        {
            if (this.CanMoveUp)
            {
                this.yOffset = this.YOffset - 1;
            }
        }

        public Point RootCenterPoint(int id)
        {
            var row    = this.ViewControl.RowFrom(id);
            var column = this.ViewControl.ColumnFrom(id);
            return new Point(
                this.ViewSettings.StartPoint.X - this.XOffset * this.ViewSettings.RootMargin.X + column * this.ViewSettings.RootMargin.X,
                this.ViewSettings.StartPoint.Y - this.YOffset * this.ViewSettings.RootMargin.Y + row    * this.ViewSettings.RootMargin.Y);
        }
    }
}