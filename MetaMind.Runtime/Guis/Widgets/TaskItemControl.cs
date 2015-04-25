namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class TaskItemControl : ViewItemControl2D
    {
        #region Constructors

        public TaskItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new TaskItemFrameControl(item);
            this.ItemSyncControl  = new ViewItemSyncControl(item);
        }

        public ItemDataFrame ExperienceFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).ExperienceFrame; } }

        public ItemDataFrame IdFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemDataFrame NameFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).NameFrame; } }

        public ItemDataFrame ProgressFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).ProgressFrame; } }

        public ViewItemSyncControl ItemSyncControl { get; private set; }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            // only need to remove from gui
            // tasks are not stored centralizedly
            this.View.Items.Remove(this.Item);

            this.Item.Dispose();
        }

        #endregion Operations

        #region Update

        public bool Locked
        {
            get
            {
                return this.Item.IsEnabled(ItemState.Item_Editing) || 
                       this.Item.IsEnabled(ItemState.Item_Pending);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // mouse and keyboard in modifier
            //-----------------------------------------------------------------
            base.UpdateInput(input, time);

            // keyboard
            //-----------------------------------------------------------------
            if (this.ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    // in pending status
                    if (this.Item.IsEnabled(ItemState.Item_Pending))
                    {
                        if (input.State.Keyboard.IsKeyTriggered(Keys.N))
                        {
                            this.ItemDataControl.EditString("Name");
                        }

                        // UNDONE: Won't work anymore
                        if (input.State.Keyboard.IsKeyTriggered(Keys.D))
                        {
                            this.ItemDataControl.EditInt("Done");
                        }

                        if (input.State.Keyboard.IsKeyTriggered(Keys.L))
                        {
                            this.ItemDataControl.EditInt("Load");
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View.Disable(ViewState.Item_Editting);
                            this.Item.Disable(ItemState.Item_Pending);
                        }
                    }

                    // Enter synchronization when
                    // Accepting input but not locked
                    if (!this.Locked)
                    {
                        // Normal status
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskEditItem))
                        {
                            this.View.Enable(ViewState.Item_Editting);
                            this.Item.Enable(ItemState.Item_Pending);
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskDeleteItem))
                        {
                            this.DeleteIt();
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Enter))
                        {
                            this.ItemSyncControl.SwitchSync();
                        }
                    }
                }
            }
        }

        #endregion Update
    }
}