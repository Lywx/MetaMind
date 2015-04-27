namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class PointGridControl : PointViewControl2D, IPointGridControl
    {
        public PointGridControl(IView view, PointGridSettings viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
            this.Region    = new PointViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);
            this.ScrollBar = new PointViewScrollBar(view, viewSettings, itemSettings, viewSettings.ScrollBarSettings);
        }

        #region Public Properties

        public PointViewRegion Region { get; protected set; }

        public PointViewScrollBar ScrollBar { get; protected set; }

        #endregion Public Properties

        #region Operations

        public override void MoveDown()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveDown();
        }

        public override void MoveLeft()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveLeft();
        }

        public override void MoveRight()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveRight();
        }

        public override void MoveUp()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveUp();
        }

        public void ScrollDown()
        {
            this.Scroll   .MoveDown();
            this.ScrollBar.Trigger();
        }

        public void ScrollUp()
        {
            this.ScrollBar.Trigger();
            this.Scroll   .MoveUp();
        }

        #endregion Operations

        #region Update Structure

        /// <remarks>
        /// All state change should be inside this methods.
        /// </remarks>>
        /// <param name="time"></param>
        public override void Update(GameTime time)
        {
            base          .Update(time);
            this.Region   .Update(time);
            this.ScrollBar.Update(time);
        }

        protected override void UpdateViewFocus()
        {
            // TODO:Moved to?
            this.View[ViewState.View_Has_Focus] = () => this.Region[RegionState.Region_Has_Focus]() || this.View[ViewState.View_Has_Selection]();
        }

        #endregion Update Structure

        #region Update Input

        public bool Locked
        {
            get { return this.View[ViewState.View_Is_Editing](); }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            this.UpdateRegionClick(input, time);
            this.UpdateMouseScroll(input);
            this.UpdateKeyboardMotion(input, time);
            this.UpdateItemInput(input, time);
        }

        protected void UpdateKeyboardMotion(IGameInputService input, GameTime time)
        {
            if (this.AcceptInput)
            {
                // Keyboard
                if (this.ViewSettings.KeyboardEnabled)
                {
                    // Movement
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastUp))
                    {
                        this.FastMoveUp();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastDown))
                    {
                        this.FastMoveDown();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.FastMoveLeft();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.FastMoveRight();
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        protected override void UpdateMouseScroll(IGameInputService input)
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

        protected void UpdateRegionClick(IGameInputService input, GameTime gameTime)
        {
            if (this.Active)
            {
                this.Region.UpdateInput(input, gameTime);
            }
        }

        #endregion Update Input

        #region Configurations

        protected virtual Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.PointStart.X,
                viewSettings.PointStart.Y,
                viewSettings.ColumnNumDisplay * itemSettings.NameFrameSize.X,
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}