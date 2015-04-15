namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;

    using Microsoft.Xna.Framework;

    public interface IPointGridControl : IPointViewControl2D
    {
        bool Locked { get; }

        PointViewRegion Region { get; }

        PointViewScrollBar ScrollBar { get; }

        void ScrollDown();

        void ScrollUp();
    }

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
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base          .Update(gameTime);
            this.Region   .Update(gameTime);
            this.ScrollBar.Update(gameTime);
        }

        protected override void UpdateViewFocus()
        {
            if (this.Region.IsEnabled(RegionState.Region_Has_Focus))
            {
                this.View.Enable(ViewState.View_Has_Focus);
            }
            else if (this.View.IsEnabled(ViewState.View_Has_Selection))
            {
                this.View.Enable(ViewState.View_Has_Focus);
            }
            else
            {
                this.View.Disable(ViewState.View_Has_Focus); 
            }
        }

        #endregion Update Structure

        #region Update Input

        public bool Locked
        {
            get { return this.View.IsEnabled(ViewState.Item_Editting); }
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.UpdateRegionClick(gameInput, gameTime);
            this.UpdateMouseScroll(gameInput);
            this.UpdateKeyboardMotion(gameInput, gameTime);
            this.UpdateItemInput(gameInput, gameTime);
        }

        protected void UpdateKeyboardMotion(IGameInput gameInput, GameTime gameTime)
        {
            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    // movement
                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.MoveRight();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Up))
                    {
                        this.MoveUp();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Down))
                    {
                        this.MoveDown();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastUp))
                    {
                        this.SuperMoveUp();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastDown))
                    {
                        this.SuperMoveDown();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastLeft))
                    {
                        this.SuperMoveLeft();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.FastRight))
                    {
                        this.SuperMoveRight();
                    }

                    if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        protected override void UpdateMouseScroll(IGameInput gameInput)
        {
            if (this.AcceptInput)
            {
                if (this.ViewSettings.MouseEnabled)
                {
                    if (gameInput.State.Mouse.IsWheelScrolledUp)
                    {
                        this.ScrollUp();
                    }

                    if (gameInput.State.Mouse.IsWheelScrolledDown)
                    {
                        this.ScrollDown();
                    }
                }
            }
        }

        protected void UpdateRegionClick(IGameInput gameInput, GameTime gameTime)
        {
            if (this.Active)
            {
                this.Region.UpdateInput(gameInput, gameTime);
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