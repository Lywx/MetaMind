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
        private IPointView2DLayout viewLayout;

        private IPointView2DLogic viewLogic;

        private PointView2DSettings viewSettings;

        private int offsetX;

        private int offsetY;

        public PointView2DScrollController(IView view)
            : base(view)
        {
        }

        #region Layering

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<PointView2DLayer>();

            this.viewLogic = viewLayer.ViewLogic;
            this.viewSettings = viewLayer.ViewSettings;
            this.viewLayout = this.viewLogic.ViewLayout;
        }

        #endregion

        private int ColumnFrom(int id)
        {
            return this.viewLayout.ColumnOf(id);
        }

        private int RowFrom(int id)
        {
            return this.viewLayout.RowOf(id);
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
                return (this.viewSettings.ColumnNumDisplay * (this.viewSettings.RowNumDisplay + this.OffsetY) < this.viewSettings.ColumnNumDisplay * this.viewSettings.RowNumMax) && 
                       (this.viewSettings.ColumnNumMax * (this.viewSettings.RowNumDisplay + this.OffsetY) < this.View.Items.Count);
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
                return this.viewSettings.RowNumDisplay * (this.viewSettings.ColumnNumDisplay + this.OffsetX)
                       < this.viewSettings.RowNumDisplay * this.viewSettings.ColumnNumMax;
            }
        }

        private bool CanMoveUp
        {
            get { return this.OffsetY > 0; }
        }

        public bool CanDisplay(int row, int column)
        {
            return this.OffsetX <= column && column < this.viewSettings.ColumnNumDisplay + this.OffsetX &&
                   this.OffsetY <= row    && row    < this.viewSettings.RowNumDisplay    + this.OffsetY;
        }

        public bool CanDisplay(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);

            return this.CanDisplay(row, column);
        }

        public bool IsDownToDisplay(int row)
        {
            return row > this.viewSettings.RowNumDisplay + this.OffsetY - 1;
        }

        public bool IsLeftToDisplay(int column)
        {
            return column < this.OffsetX;
        }

        public bool IsRightToDisplay(int column)
        {
            return column > this.viewSettings.ColumnNumDisplay + this.OffsetX - 1;
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
                this.viewSettings.Position.X - this.OffsetX * this.viewSettings.Margin.X + column * this.viewSettings.Margin.X,
                this.viewSettings.Position.Y - this.OffsetY * this.viewSettings.Margin.Y + row * this.viewSettings.Margin.Y);
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