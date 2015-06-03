namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
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

        public bool CanDisplay(int row)
        {
            var column = row;
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

        public Vector2 Position(int row)
        {
            return new Vector2(
                this.viewSettings.Direction == ViewDirection.Normal
                    ? this.viewSettings.Position.X - (this.OffsetX * this.viewSettings.Margin.X) + row * this.viewSettings.Margin.X
                    : this.viewSettings.Position.X + (this.OffsetX * this.viewSettings.Margin.X) - row * this.viewSettings.Margin.X,
                this.viewSettings.Position.Y);
        }

        public void Zoom(int row)
        {
            if (!this.CanDisplay(row))
            {
                var column = row;

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