// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskViewControl.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Regions;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts.Tasks;

    using Microsoft.Xna.Framework;

    public class TaskViewControl : PointGridControl
    {
        #region Constructors

        public TaskViewControl(IView view, TaskViewSettings viewSettings, TaskItemSettings itemSettings, TaskItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        #endregion Constructors

        #region Operations

        public void AddItem(Task entry)
        {
            this.View.Items.Add(new ViewItemExchangable(this.View, this.ViewSettings, this.ItemSettings, this.ItemFactory, entry));
        }

        #endregion Operations

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime gameTime)
        {
            this.UpdateRegionClick(input, gameTime);
            this.UpdateMouseScroll(input);
            this.UpdateKeyboardMotion(input, gameTime);

            if (this.AcceptInput)
            {
                if (this.ViewSettings.KeyboardEnabled)
                {
                    // list management
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskCreateItem))
                    {
                        this.AddItem();

                        // auto select new item
                        this.Selection.Select(this.View.Items.Count - 1);
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskDeleteItem))
                    {
                        // itme deletion is handled by item control
                        // auto select last item
                        if (View.Items.Count > 1)
                        {
                            // this will be called before item deletion
                            if (this.Selection.SelectedId != null && 
                                this.Selection.SelectedId > View.Items.Count - 2)
                            {
                                this.Selection.Select(View.Items.Count - 2);
                            }
                        }
                    }
                }
            }

            this.UpdateItemInput(input, gameTime);
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

        protected override Rectangle RegionPositioning(dynamic viewSettings, dynamic itemSettings)
        {
            return new Rectangle(
                viewSettings.PointStart.X,
                viewSettings.PointStart.Y,
                viewSettings.ColumnNumDisplay * itemSettings.NameFrameSize.X,
                viewSettings.RowNumDisplay    * (itemSettings.NameFrameSize.Y + itemSettings.IdFrameSize.Y));
        }

        #endregion
    }
}