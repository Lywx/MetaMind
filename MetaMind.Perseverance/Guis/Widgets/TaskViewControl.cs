// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskViewControl.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.ViewItems;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    using Microsoft.Xna.Framework;

    public class TaskViewControl : ViewControl2D
    {
        #region Constructors

        public TaskViewControl(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings)
            : base(view, viewSettings, itemSettings)
        {
            this.Region      = new ViewRegion(view, viewSettings, itemSettings, this.RegionPositioning);
            this.ScrollBar   = new ViewScrollBar(view, viewSettings, itemSettings, viewSettings.ScrollBarSettings);
            this.ItemFactory = new TaskItemFactory();
        }

        #endregion Constructors

        #region Public Properties

        public IViewItemFactory ItemFactory { get; protected set; }

        public ViewRegion Region { get; protected set; }

        public ViewScrollBar ScrollBar { get; protected set; }

        #endregion Public Properties

        #region Operations

        public void AddItem(TaskEntry entry)
        {
            this.View.Items.Add(new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry));
        }

        public void AddItem()
        {
            this.View.Items.Add(
                new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory));
        }

        public void MoveDown()
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

        public void MoveUp()
        {
            this.ScrollBar.Trigger();
            this.Selection.MoveUp();
        }

        #endregion Operations

        #region Update

        public bool Locked
        {
            get { return this.View.IsEnabled(ViewState.Item_Editting); }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse
            // -----------------------------------------------------------------
            // region
            if (this.Active)
            {
                this.Region.UpdateInput(gameTime);
            }

            if (this.AcceptInput)
            {
                // mouse
                // ---------------------------------------------------------------------
                if (this.ViewSettings.MouseEnabled)
                {
                    // scroll
                    if (InputSequenceManager.Mouse.IsWheelScrolledUp)
                    {
                        this.ScrollBar.Trigger();
                        this.Scroll   .MoveUp();
                    }

                    if (InputSequenceManager.Mouse.IsWheelScrolledDown)
                    {
                        this.Scroll   .MoveDown();
                        this.ScrollBar.Trigger();
                    }
                }

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

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SUp))
                    {
                        for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
                        {
                            this.MoveUp();
                        }
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.SDown))
                    {
                        for (var i = 0; i < this.ViewSettings.RowNumDisplay; i++)
                        {
                            this.MoveDown();
                        }
                    }

                    // escape
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                    {
                        this.Selection.Clear();
                    }

                    // list management
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskCreateItem))
                    {
                        this.AddItem();

                        // auto select new item
                        this.Selection.Select(this.View.Items.Count - 1);
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (this.View.Items.Count > 1)
                        {
                            // this will be called before item deletion
                            this.Selection.Select(this.View.Items.Count - 2);
                        }
                    }
                }
            }

            // item input
            // -----------------------------------------------------------------
            foreach (var item in this.View.Items.ToArray())
            {
                item.UpdateInput(gameTime);
            }
        }

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

        #endregion Update

        #region Configurations

        private Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.StartPoint.X,
                viewSettings.StartPoint.Y,
                viewSettings.ColumnNumDisplay * itemSettings.NameFrameSize.X,
                viewSettings.RowNumDisplay    * (itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y));
        }


        #endregion
    }
}