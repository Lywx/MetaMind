namespace MetaMind.Runtime.Guis.Widgets
{
    using System;
    using Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class TaskItemLogic : PointView2DItemLogic
    {
        #region Constructors

        public TaskItemLogic(IViewItem item)
            : base(item)
        {
            this.ItemFrame = new ExperienceItemFrameControl(item);
        }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            // only need to remove from gui
            // tasks are not stored centralizedly
            this.View.ViewLogic.Remove(this.Item);

            this.Item.Dispose();
        }

        #endregion Operations

        #region Update

        public Func<bool> ItemIsPending
        {
            get
            {
                return () => this.Item[ItemState.Item_Is_Editing]() ||
                             this.Item[ItemState.Item_Is_Pending]();
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // Mouse and keyboard in modifier
            base.UpdateInput(input, time);

            // Keyboard
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                if (this.ItemIsInputting())
                {
                    // In pending status
                    if (this.Item[ItemState.Item_Is_Pending]())
                    {
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View[ViewState.View_Is_Editing] = () => false;
                            this.Item[ItemState.Item_Is_Pending] = () => false;
                        }
                    }

                    // Enter synchronization when
                    // Accepting input but not locked
                    if (!this.ItemIsPending())
                    {
                        // Normal status
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskEditItem))
                        {
                            this.View[ViewState.View_Is_Editing] = () => true;
                            this.Item[ItemState.Item_Is_Pending] = () => true;
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskDeleteItem))
                        {
                            this.DeleteIt();
                        }
                    }
                }
            }
        }

        #endregion Update
    }
}