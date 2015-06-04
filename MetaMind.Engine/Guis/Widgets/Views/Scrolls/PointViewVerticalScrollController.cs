namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using Layers;
    using Settings;

    using Microsoft.Xna.Framework;

    public class PointViewVerticalScrollController : ViewComponent, IPointViewVerticalScrollController
    {
        private PointViewVerticalSettings viewSettings;

        public PointViewVerticalScrollController(IView view)
            : base(view)
        {
        }

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointViewVerticalLayer>();
            this.viewSettings = viewLayer.ViewSettings;
        }

        public int OffsetY { get; private set; }

        private bool CanMoveUp
        {
            get
            {
                return this.OffsetY > 0;
            }
        }

        private bool CanMoveDown
        {
            get
            {
                return (this.viewSettings.RowNumDisplay + this.OffsetY) < this.View.ItemsRead.Count;
            }
        }

        public bool CanDisplay(int row)
        {
            return this.OffsetY <= row && row < this.viewSettings.RowNumDisplay + this.OffsetY;
        }

        public bool IsUpToDisplay(int row)
        {
            return row < this.OffsetY - 1;
        }

        public bool IsDownToDisplay(int row)
        {
            return row > this.viewSettings.RowNumDisplay + this.OffsetY;
        }

        public void MoveUp()
        {
            if (this.CanMoveUp)
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
            if (this.CanMoveDown)
            {
                ++this.OffsetY;
            }
        }

        public virtual Vector2 Position(int row)
        {
            return new Vector2(
                this.viewSettings.Position.X,
                this.viewSettings.Direction == ViewDirection.Normal
                    ? this.viewSettings.Position.Y - (this.OffsetY * this.viewSettings.Margin.Y) + row * this.viewSettings.Margin.Y
                    : this.viewSettings.Position.Y + (this.OffsetY * this.viewSettings.Margin.Y) - row * this.viewSettings.Margin.Y);
        }

        public virtual void Zoom(int row)
        {
            if (!this.CanDisplay(row))
            {
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