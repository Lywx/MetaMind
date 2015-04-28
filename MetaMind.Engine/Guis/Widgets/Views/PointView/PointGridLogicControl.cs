namespace MetaMind.Engine.Guis.Widgets.Views.PointView
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointGridLogicControl : PointView2DLogicControl, IPointGridLogicControl
    {
        public PointGridLogicControl(IView view, PointGridSettings viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.Region    = new ViewRegion(this.RegionBounds);
            this.Scrollbar = new ViewVerticalScrollBar(viewSettings, this.Scroll, this, this.Region, viewSettings.ScrollbarSettings);

            this.View[ViewState.View_Has_Focus] = () => this.Region[RegionState.Region_Has_Focus]() || this.View[ViewState.View_Has_Selection]();
        }

        #region Public Properties

        public ViewRegion Region { get; protected set; }

        public ViewVerticalScrollBar Scrollbar { get; protected set; }

        #endregion Public Properties

        #region Operations

        public override void MoveDown()
        {
            this.Scrollbar.Trigger();
            this.Selection.MoveDown();
        }

        public override void MoveLeft()
        {
            this.Scrollbar.Trigger();
            this.Selection.MoveLeft();
        }

        public override void MoveRight()
        {
            this.Scrollbar.Trigger();
            this.Selection.MoveRight();
        }

        public override void MoveUp()
        {
            this.Scrollbar.Trigger();
            this.Selection.MoveUp();
        }

        public void ScrollDown()
        {
            this.Scrollbar.Trigger();
            this.Scroll   .MoveDown();
        }

        public void ScrollUp()
        {
            this.Scrollbar.Trigger();
            this.Scroll   .MoveUp();
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
            if (this.AcceptInput)
            {
                if (this.ViewSettings.MouseEnabled)
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
            if (this.Active)
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