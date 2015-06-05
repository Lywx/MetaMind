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

        private int columnOffset;

        private int rowOffset;

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

        public int ColumnOffset
        {
            get { return this.columnOffset; }
        }

        public int RowOffset
        {
            get { return this.rowOffset; }
        }

        private bool CanMoveDown
        {
            get
            {
                return (this.viewSettings.ColumnNumDisplay * (this.viewSettings.RowNumDisplay + this.RowOffset) < this.viewSettings.ColumnNumDisplay * this.viewSettings.RowNumMax) && 
                       (this.viewSettings.ColumnNumMax * (this.viewSettings.RowNumDisplay + this.RowOffset) < this.View.ItemsRead.Count);
            }
        }

        private bool CanMoveLeft
        {
            get { return this.ColumnOffset > 0; }
        }

        private bool CanMoveRight
        {
            get
            {
                return this.viewSettings.RowNumDisplay * (this.viewSettings.ColumnNumDisplay + this.ColumnOffset)
                       < this.viewSettings.RowNumDisplay * this.viewSettings.ColumnNumMax;
            }
        }

        private bool CanMoveUp
        {
            get { return this.RowOffset > 0; }
        }

        public bool CanDisplay(int row, int column)
        {
            return this.ColumnOffset <= column && column < this.viewSettings.ColumnNumDisplay + this.ColumnOffset &&
                   this.RowOffset    <= row    && row    < this.viewSettings.RowNumDisplay    + this.RowOffset;
        }

        public bool CanDisplay(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);

            return this.CanDisplay(row, column);
        }

        public bool IsDownToDisplay(int id)
        {
            return id > this.viewSettings.RowNumDisplay + this.RowOffset - 1;
        }

        public bool IsLeftToDisplay(int id)
        {
            return id < this.ColumnOffset;
        }

        public bool IsRightToDisplay(int id)
        {
            return id > this.viewSettings.ColumnNumDisplay + this.ColumnOffset - 1;
        }

        public bool IsUpToDisplay(int id)
        {
            return id < this.RowOffset;
        }

        public void MoveDown()
        {
            if (this.CanMoveDown)
            {
                this.rowOffset = this.RowOffset + 1;
            }
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                this.columnOffset = this.ColumnOffset - 1;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                this.columnOffset = this.ColumnOffset + 1;
            }
        }

        public void MoveUp()
        {
            if (this.CanMoveUp)
            {
                this.rowOffset = this.RowOffset - 1;
            }
        }

        public void MoveUpToTop()
        {
            this.rowOffset = 0;
        }

        public Vector2 Position(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);
            return new Vector2(
                this.viewSettings.Position.X - this.ColumnOffset * this.viewSettings.Margin.X + column * this.viewSettings.Margin.X,
                this.viewSettings.Position.Y - this.RowOffset * this.viewSettings.Margin.Y + row * this.viewSettings.Margin.Y);
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