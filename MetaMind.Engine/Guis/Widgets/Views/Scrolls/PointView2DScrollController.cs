namespace MetaMind.Engine.Guis.Widgets.Views.Scrolls
{
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;

    using Microsoft.Xna.Framework;

    /// <summary>
    /// Dependent on PointView2DSettings IPointView2DLogic
    /// </summary>
    public class PointView2DScrollController : ViewComponent, IPointView2DScrollController
    {
        private readonly PointView2DLayer viewLayer;

        private int offsetX;

        private int offsetY;

        public PointView2DScrollController(IView view)
            : base(view)
        {
            this.viewLayer = this.ViewGetLayer<PointView2DLayer>();
        }

        #region Indirect Dependency
        protected PointView2DSettings ViewSettings
        {
            get { return this.viewLayer.ViewSettings; }
        }

        protected IPointView2DLogic ViewLogic
        {
            get { return this.viewLayer.ViewLogic; }
        }

        protected IPointView2DLayout ViewLayout
        {
            get { return this.ViewLogic.ViewLayout; }
        }

        #endregion

        private int ColumnFrom(int id)
        {
            return this.ViewLayout.ColumnFrom(id);
        }

        private int RowFrom(int id)
        {
            return this.ViewLayout.RowFrom(id);
        }

        public int OffsetX
        {
            get { return this.offsetX; }
        }

        public int OffsetY
        {
            get { return this.offsetY; }
        }

        private bool CanMoveDown
        {
            get
            {
                return (this.ViewSettings.ColumnNumDisplay * (this.ViewSettings.RowNumDisplay + this.OffsetY) < this.ViewSettings.ColumnNumDisplay * this.ViewSettings.RowNumMax) && 
                       (this.ViewSettings.ColumnNumMax * (this.ViewSettings.RowNumDisplay + this.OffsetY) < this.View.Items.Count);
            }
        }

        private bool CanMoveLeft
        {
            get { return this.OffsetX > 0; }
        }

        private bool CanMoveRight
        {
            get
            {
                return this.ViewSettings.RowNumDisplay * (this.ViewSettings.ColumnNumDisplay + this.OffsetX)
                       < this.ViewSettings.RowNumDisplay * this.ViewSettings.ColumnNumMax;
            }
        }

        private bool CanMoveUp
        {
            get { return this.OffsetY > 0; }
        }

        public bool CanDisplay(int row, int column)
        {
            return this.OffsetX <= column && column < this.ViewSettings.ColumnNumDisplay + this.OffsetX &&
                   this.OffsetY <= row    && row    < this.ViewSettings.RowNumDisplay    + this.OffsetY;
        }

        public bool CanDisplay(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);

            return this.CanDisplay(row, column);
        }

        public bool IsDownToDisplay(int row)
        {
            return row > this.ViewSettings.RowNumDisplay + this.OffsetY - 1;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.OffsetX;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.ViewSettings.ColumnNumDisplay + this.OffsetX - 1;
        }

        public bool IsUpToDisplay(int row)
        {
            return row < this.OffsetY;
        }

        public void MoveDown()
        {
            if (this.CanMoveDown)
            {
                this.offsetY = this.OffsetY + 1;
            }
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                this.offsetX = this.OffsetX - 1;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                this.offsetX = this.OffsetX + 1;
            }
        }

        public void MoveUp()
        {
            if (this.CanMoveUp)
            {
                this.offsetY = this.OffsetY - 1;
            }
        }

        public void MoveUpToTop()
        {
            this.offsetY = 0;
        }

        public Vector2 Position(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);
            return new Vector2(
                this.ViewSettings.Position.X - this.OffsetX * this.ViewSettings.Margin.X + column * this.ViewSettings.Margin.X,
                this.ViewSettings.Position.Y - this.OffsetY * this.ViewSettings.Margin.Y + row * this.ViewSettings.Margin.Y);
        }

        public void Zoom(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);

            if (!this.CanDisplay(row, column))
            {
                while (this.IsLeftToDisplay(column))
                {
                    this.MoveLeft();
                }

                while (this.IsRightToDisplay(column))
                {
                    this.MoveRight();
                }

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