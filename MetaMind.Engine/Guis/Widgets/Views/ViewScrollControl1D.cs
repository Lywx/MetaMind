namespace MetaMind.Engine.Guis.Widgets.Views
{
    using MetaMind.Engine.Guis.Widgets.Items;

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
            get { return (this.ViewSettings.ColumnNumDisplay + this.scroll) < this.View.Items.Count; }
        }

        public bool CanDisplay(int id)
        {
            return this.scroll <= id && id < this.ViewSettings.ColumnNumDisplay + this.scroll;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.scroll - 1;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.ViewSettings.ColumnNumDisplay + this.scroll;
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
                this.ViewSettings.Direction == ViewSettings1D.ScrollDirection.Right ?
                this.ViewSettings.StartPoint.X - (this.scroll * this.ViewSettings.RootMargin.X) + id * this.ViewSettings.RootMargin.X :
                this.ViewSettings.StartPoint.X + (this.scroll * this.ViewSettings.RootMargin.X) - id * this.ViewSettings.RootMargin.X,
                this.ViewSettings.StartPoint.Y);
        }
    }
}