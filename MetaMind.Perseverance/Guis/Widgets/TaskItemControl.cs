namespace MetaMind.Perseverance.Guis.Widgets
{
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
            if (item.View.Parent is MotivationTaskTracer)
            {
                this.ItemViewControl = new TaskItemViewControlInMotivation(item);
            }

        }

        public ItemEntryFrame ExperienceFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).ExperienceFrame; } }

        public ItemEntryFrame IdFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).NameFrame; } }

        public ItemEntryFrame ProgressFrame { get { return ((TaskItemFrameControl)this.ItemFrameControl).ProgressFrame; } }

        public TaskItemSyncControl ItemSyncControl { get; private set; }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            // only need to remove from gui
            // tasks are not stored centralizedly
            this.View.Items.Remove(this.Item);
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

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse and keyboard in modifier
            //-----------------------------------------------------------------
            base.UpdateInput(gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (this.ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    // in pending status
                    if (this.Item.IsEnabled(ItemState.Item_Pending))
                    {
                        if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.N))
                        {
                            this.ItemDataControl.EditString("Name");
                        }

                        if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.D))
                        {
                            this.ItemDataControl.EditInt("Done");
                        }

                        if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.E))
                        {
                            this.ItemDataControl.EditExperience("Experience");
                        }

                        if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.L))
                        {
                            this.ItemDataControl.EditInt("Load");
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                        {
                            this.View.Disable(ViewState.Item_Editting);
                            this.Item.Disable(ItemState.Item_Pending);
                        }
                    }

                    // enter synchronization when
                    // accepting input but not locked
                    if (!this.Locked)
                    {
                        // normal status
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskEditItem))
                        {
                            this.View.Enable(ViewState.Item_Editting);
                            this.Item.Enable(ItemState.Item_Pending);
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskDeleteItem))
                        {
                            this.DeleteIt();
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Enter))
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