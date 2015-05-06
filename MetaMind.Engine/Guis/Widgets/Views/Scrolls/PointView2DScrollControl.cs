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
    public class PointView2DScrollControl : ViewComponent, IPointView2DScrollControl
    {
        private readonly PointView2DSettings viewSettings;

        private readonly IPointView2DLogic viewLogic;

        private int offsetX;

        private int offsetY;

        private IPointView2DLayout viewLayout;

        public PointView2DScrollControl(IView view)
            : base(view)
        {
            var viewLayer     = this.ViewGetLayer<PointView2DLayer>();
            this.viewSettings = viewLayer.ViewSettings;
            this.viewLogic    = viewLayer.ViewLogic;
            this.viewLayout   = this.viewLogic.ViewLayout;
        }

        #region Dependency

        private int ColumnFrom(int id)
        {
            return this.viewLayout.ColumnFrom(id);
        }

        private int RowFrom(int id)
        {
            return this.viewLayout.RowFrom(id);
        }

        #endregion

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
                this.viewSettings.PointStart.X - this.OffsetX * this.viewSettings.PointMargin.X + column * this.viewSettings.PointMargin.X,
                this.viewSettings.PointStart.Y - this.OffsetY * this.viewSettings.PointMargin.Y + row * this.viewSettings.PointMargin.Y);
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