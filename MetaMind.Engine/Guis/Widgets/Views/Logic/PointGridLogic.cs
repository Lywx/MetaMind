namespace MetaMind.Engine.Guis.Widgets.Views.Logic
{
    using MetaMind.Engine.Guis.Widgets.Items.Factories;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views.Extensions;
    using MetaMind.Engine.Guis.Widgets.Views.Scrolls;
    using MetaMind.Engine.Guis.Widgets.Views.Selections;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Guis.Widgets.Views.Visuals;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointGridLogic : PointView2DLogic, IPointGridLogic
    {
        private IPointView2DSelectionControl viewSelection;

        private IPointView2DScrollControl viewScroll;

        public PointGridLogic(IView view, PointGridSettings viewSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemFactory)
        {
            var viewLayer = this.ViewGetLayer<PointView2DLayer>();
            this.viewSelection = viewLayer.ViewSelection;
            this.viewScroll = viewLayer.ViewScroll;

            this.Region    = new ViewRegion(this.RegionBounds);
            this.Scrollbar = new ViewVerticalScrollBar(viewSettings, viewLayer.ViewScroll, this, this.Region, viewSettings.ScrollbarSettings);

            this.View[ViewState.View_Has_Focus] = () => this.Region[RegionState.Region_Has_Focus]() || this.View[ViewState.View_Has_Selection]();
        }

        #region Public Properties

        #endregion Public Properties

        #region Operations

        public override void MoveDown()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveDown();
        }

        public override void MoveLeft()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveLeft();
        }

        public override void MoveRight()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveRight();
        }

        public override void MoveUp()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewSelection.MoveUp();
        }

        public void ScrollDown()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewScroll.MoveDown();
        }

        public void ScrollUp()
        {
            this.ViewGetComponent<ViewVerticalScrollBar>("verticalScrollbar").Trigger();
            this.viewScroll.MoveUp();
        }

        #endregion Operations

        #region Update Structure

        public override void Update(GameTime time)
        {
            base          .Update(time);
            this.Region   .Update(time);
            this.Scrollbar.Update(time);
        }
        
        #endregion Update Structure

        #region Update Input

        public bool Locked
        {
            get { return this.View[ViewState.View_Is_Editing](); }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateInputOfRegion(input, time);
            this.UpdateInputOfMouse(input, time);
            this.UpdateInputOfKeyboard(input, time);
            this.UpdateInputOfItems(input, time);
        }

        protected override void UpdateInputOfMouse(IGameInputService input, GameTime time)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                if (this.viewSettings.MouseEnabled)
                {
                    if (input.State.Mouse.IsWheelScrolledUp)
                    {
                        this.ScrollUp();
                    }

                    if (input.State.Mouse.IsWheelScrolledDown)
                    {
                        this.ScrollDown();
                    }
                }
            }
        }

        protected void UpdateInputOfRegion(IGameInputService input, GameTime gameTime)
        {
            if (this.View[ViewState.View_Is_Inputting]())
            {
                this.Region.UpdateInput(input, gameTime);
            }
        }

        #endregion Update Input

        #region Configurations

        protected virtual Rectangle RegionBounds()
        {
            return new Rectangle(
                this.ViewSettings.PointStart.X,
                this.ViewSettings.PointStart.Y,
                this.ViewSettings.ColumnNumDisplay * this.ItemSettings.NameFrameSize.X,
                this.ViewSettings.RowNumDisplay    * this.ItemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}