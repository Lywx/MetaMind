namespace MetaMind.Engine.Gui.Controls.Views.Scrolls
{
    using Layers;
    using Layouts;
    using Logic;
    using Microsoft.Xna.Framework;
    using Settings;

    /// <summary>
    /// Dependent on PointView2DSettings IPointView2DLogic
    /// </summary>
    public class PointView2DScrollController : ViewComponent, IPointView2DScrollController
    {
        private IPointView2DLayout viewLayout;

        private IPointView2DLogic viewLogic;

        private PointView2DSettings viewSettings;

        public PointView2DScrollController(IView view)
            : base(view)
        {
        }

        #region Layering

        public override void Initialize()
        {
            base.Initialize();

            var viewLayer = this.GetViewLayer<PointView2DLayer>();

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

        public int ColumnOffset { get; set; }

        public int RowOffset { get; set; }

        private bool CanMoveDown
        {
            get
            {
                return (this.viewSettings.ViewColumnDisplay * (this.viewSettings.ViewRowDisplay + this.RowOffset) < this.viewSettings.ViewColumnDisplay * this.viewSettings.ViewRowMax) && 
                       (this.viewSettings.ViewColumnMax * (this.viewSettings.ViewRowDisplay + this.RowOffset) < this.View.ItemsRead.Count);
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
                return this.viewSettings.ViewRowDisplay * (this.viewSettings.ViewColumnDisplay + this.ColumnOffset)
                       < this.viewSettings.ViewRowDisplay * this.viewSettings.ViewColumnMax;
            }
        }

        private bool CanMoveUp => this.RowOffset > 0;

        public bool CanDisplay(int row, int column)
        {
            return this.ColumnOffset <= column && column < this.viewSettings.ViewColumnDisplay + this.ColumnOffset &&
                   this.RowOffset    <= row    && row    < this.viewSettings.ViewRowDisplay    + this.RowOffset;
        }

        public bool CanDisplay(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);

            return this.CanDisplay(row, column);
        }

        public bool IsDownToDisplay(int id)
        {
            return id > this.viewSettings.ViewRowDisplay + this.RowOffset - 1;
        }

        public bool IsLeftToDisplay(int id)
        {
            return id < this.ColumnOffset;
        }

        public bool IsRightToDisplay(int id)
        {
            return id > this.viewSettings.ViewColumnDisplay + this.ColumnOffset - 1;
        }

        public bool IsUpToDisplay(int id)
        {
            return id < this.RowOffset;
        }

        public void MoveDown()
        {
            if (this.CanMoveDown)
            {
                this.RowOffset = this.RowOffset + 1;
            }
        }

        public void MoveLeft()
        {
            if (this.CanMoveLeft)
            {
                this.ColumnOffset = this.ColumnOffset - 1;
            }
        }

        public void MoveRight()
        {
            if (this.CanMoveRight)
            {
                this.ColumnOffset = this.ColumnOffset + 1;
            }
        }

        public void MoveUp()
        {
            if (this.CanMoveUp)
            {
                this.RowOffset = this.RowOffset - 1;
            }
        }

        public void MoveUpToTop()
        {
            this.RowOffset = 0;
        }

        public Vector2 Position(int id)
        {
            var row    = this.RowFrom(id);
            var column = this.ColumnFrom(id);
            return new Vector2(
                this.viewSettings.ViewPosition.X - this.ColumnOffset * this.viewSettings.ItemMargin.X + column * this.viewSettings.ItemMargin.X,
                this.viewSettings.ViewPosition.Y - this.RowOffset * this.viewSettings.ItemMargin.Y + row * this.viewSettings.ItemMargin.Y);
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

        public void Reset()
        {
            this.RowOffset    = 0;
            this.ColumnOffset = 0;
        }
    }
}