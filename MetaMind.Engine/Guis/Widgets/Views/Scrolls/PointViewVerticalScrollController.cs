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

        public int RowOffset { get; protected set; }

        protected virtual bool CanMoveUp
        {
            get
            {
                return this.RowOffset > 0;
            }
        }

        protected virtual bool CanMoveDown
        {
            get
            {
                return (this.ViewSettings.ViewRowDisplay + this.RowOffset) < this.View.ItemsRead.Count;
            }
        }

        protected PointViewVerticalSettings ViewSettings
        {
            get { return this.viewSettings; }
        }

        public bool CanDisplay(int id)
        {
            var row = id;
            return this.RowOffset <= row && row < this.ViewSettings.ViewRowDisplay + this.RowOffset;
        }

        public virtual bool IsUpToDisplay(int id)
        {
            var row = id;
            return row < this.RowOffset - 1;
        }

        public virtual bool IsDownToDisplay(int id)
        {
            var row = id;
            return row > this.ViewSettings.ViewRowDisplay + this.RowOffset;
        }

        public virtual void MoveUp()
        {
            if (this.CanMoveUp)
            {
                --this.RowOffset;
            }
        }

        public void MoveUpToTop()
        {
            this.RowOffset = 0;
        }

        public virtual void MoveDown()
        {
            if (this.CanMoveDown)
            {
                ++this.RowOffset;
            }
        }

        public virtual Vector2 Position(int row)
        {
            return new Vector2(
                this.ViewSettings.ViewPosition.X,
                this.ViewSettings.ViewDirection == ViewDirection.Normal
                    ? this.ViewSettings.ViewPosition.Y - (this.RowOffset * this.ViewSettings.ItemMargin.Y) + row * this.ViewSettings.ItemMargin.Y
                    : this.ViewSettings.ViewPosition.Y + (this.RowOffset * this.ViewSettings.ItemMargin.Y) - row * this.ViewSettings.ItemMargin.Y);
        }

        public virtual void Zoom(int id)
        {
            if (!this.CanDisplay(id))
            {
                while (this.IsUpToDisplay(id))
                {
                    this.MoveUp();
                }

                while (this.IsDownToDisplay(id))
                {
                    this.MoveDown();
                }
            }
        }
    }
}