namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Perseverance.Guis.Modules;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class TaskItemControl : ViewItemControl2D
    {
        #region Constructors

        public TaskItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new TaskItemFrameControl(item);
            this.ItemSyncControl  = new TaskItemSyncControl(item);

            // only add this control when 
            if (item.View.Parent is TaskModule)
            {
                this.ItemViewControl = new TaskItemViewControlInMotivation(item);
            }
        }

        public ItemDataFrame ExperienceFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).ExperienceFrame; } }

        public ItemDataFrame IdFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemDataFrame NameFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).NameFrame; } }

        public ItemDataFrame ProgressFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).ProgressFrame; } }

        public TaskItemSyncControl ItemSyncControl { get; private set; }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            // only need to remove from gui
            // tasks are not stored centralizedly
            View.Items.Remove(Item);

            Item.Dispose();
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

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            // mouse and keyboard in modifier
            //-----------------------------------------------------------------
            base.UpdateInput(gameInput, gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    // in pending status
                    if (this.Item.IsEnabled(ItemState.Item_Pending))
                    {
                        if (gameInput.State.Keyboard.IsKeyTriggered(Keys.N))
                        {
                            this.ItemDataControl.EditString("Name");
                        }

                        // UNDONE: Won't work anymore
                        if (gameInput.State.Keyboard.IsKeyTriggered(Keys.D))
                        {
                            this.ItemDataControl.EditInt("Done");
                        }

                        if (gameInput.State.Keyboard.IsKeyTriggered(Keys.E))
                        {
                            this.ItemDataControl.EditExperience("SynchronizationSpan");
                        }

                        if (gameInput.State.Keyboard.IsKeyTriggered(Keys.L))
                        {
                            this.ItemDataControl.EditInt("Load");
                        }

                        if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            View.Disable(ViewState.Item_Editting);
                            this.Item.Disable(ItemState.Item_Pending);
                        }
                    }

                    // enter synchronization when
                    // accepting input but not locked
                    if (!this.Locked)
                    {
                        // normal status
                        if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.TaskEditItem))
                        {
                            View.Enable(ViewState.Item_Editting);
                            this.Item.Enable(ItemState.Item_Pending);
                        }

                        if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.TaskDeleteItem))
                        {
                            this.DeleteIt();
                        }

                        if (gameInput.State.Keyboard.IsActionTriggered(KeyboardActions.Enter))
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