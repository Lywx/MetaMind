namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Layers;
    using Settings;

    using Microsoft.Xna.Framework;

    public class PointViewVerticalScrollController : ViewComponent, IPointViewVerticalScrollController
    {
        private readonly PointViewHorizontalSettings viewSettings;

        public PointViewVerticalScrollController(IView view)
            : base(view)
        {
            this.viewSettings = this.ViewGetLayer<PointViewHorizontalLayer>().ViewSettings;
        }

        public int OffsetY { get; private set; }

        private bool CanMoveLeft
        {
            get
            {
                return this.OffsetY > 0;
            }
        }

        private bool CanMoveRight
        {
            get
            {
                return (this.viewSettings.ColumnNumDisplay + this.OffsetY) < this.View.Items.Count;
            }
        }

        public bool CanDisplay(int id)
        {
            var row = id;
            return this.OffsetY <= row && row < this.viewSettings.ColumnNumDisplay + this.OffsetY;
        }

        public bool IsUpToDisplay(int row)
        {
            return row < this.OffsetY - 1;
        }

        public bool IsDownToDisplay(int row)
        {
            return row > this.viewSettings.ColumnNumDisplay + this.OffsetY;
        }

        public void MoveUp()
        {
            if (this.CanMoveLeft)
            {
                --this.OffsetY;
            }
        }

        public void MoveUpToTop()
        {
            this.OffsetY = 0;
        }

        public void MoveDown()
        {
            if (this.CanMoveRight)
            {
                ++this.OffsetY;
            }
        }

        public Vector2 Position(int id)
        {
            var row = id;
            return new Vector2(
                this.viewSettings.Position.X,
                this.viewSettings.Direction == ViewDirection.Normal
                    ? this.viewSettings.Position.Y - (this.OffsetY * this.viewSettings.Margin.Y) + row * this.viewSettings.Margin.Y
                    : this.viewSettings.Position.Y + (this.OffsetY * this.viewSettings.Margin.Y) - row * this.viewSettings.Margin.Y);
        }

        public void Zoom(int id)
        {
            if (!this.CanDisplay(id))
            {
                var row = id;

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