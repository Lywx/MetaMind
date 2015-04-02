namespace MetaMind.Engine.Guis.Widgets.Views
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;

    using Microsoft.Xna.Framework;

    public interface IGridControl : IPointViewControl2D
    {
        bool Locked { get; }

        PointViewRegion Region { get; }

        PointViewScrollBar ScrollBar { get; }

        void ScrollDown();

        void ScrollUp();
    }

    public class GridControl : PointViewControl2D, IGridControl
    {
        public GridControl(IView view, GridSettings viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
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
        public override void UpdateStructure(GameTime gameTime)
        {
            base          .UpdateStructure(gameTime);
            this.Region   .UpdateStructure(gameTime);
            this.ScrollBar.UpdateStructure(gameTime);
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

        public override void UpdateInput(GameTime gameTime)
        {
            this.UpdateRegionClick(gameTime);
            this.UpdateMouseScroll();
            this.UpdateKeyboardMotion(gameTime);
            this.UpdateItemInput(gameTime);
        }

        protected void UpdateKeyboardMotion(GameTime gameTime)
        {
            if (this.AcceptInput)
            {
                // keyboard
                // ---------------------------------------------------------------------
                if (this.ViewSettings.KeyboardEnabled)
                {
                    // movement
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Left))
                    {
                        this.MoveLeft();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Right))
                    {
                        this.MoveRight();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Up))
                    {
                        this.MoveUp();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Down))
                    {
                        this.MoveDown();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastUp))
                    {
                        this.SuperMoveUp();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastDown))
                    {
                        this.SuperMoveDown();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastLeft))
                    {
                        this.SuperMoveLeft();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.FastRight))
                    {
                        this.SuperMoveRight();
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                    {
                        this.Selection.Clear();
                    }
                }
            }
        }

        protected override void UpdateMouseScroll()
        {
            if (this.AcceptInput)
            {
                if (this.ViewSettings.MouseEnabled)
                {
                    if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                    {
                        this.ScrollUp();
                    }

                    if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                    {
                        this.ScrollDown();
                    }
                }
            }
        }

        protected void UpdateRegionClick(GameTime gameTime)
        {
            if (this.Active)
            {
                this.Region.UpdateInput(gameTime);
            }
        }

        #endregion Update Input

        #region Configurations

        protected virtual Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * itemSettings.NameFrameSize.X,
                viewSettings.RowNumDisplay    * itemSettings.NameFrameSize.Y);
        }

        #endregion Configurations
    }
}