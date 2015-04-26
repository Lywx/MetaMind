namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements;
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
            this.ItemFrameControl = new ExperienceItemFrameControl(item);
            this.ItemSyncControl  = new ViewItemSyncControl(item);
        }

        public PickableFrame ExperienceFrame { get { return ((ExperienceItemFrameControl)this.ItemFrameControl).ExperienceFrame; } }

        public PickableFrame IdFrame { get { return ((ExperienceItemFrameControl)this.ItemFrameControl).LHoldFrame; } }

        public PickableFrame NameFrame { get { return ((ExperienceItemFrameControl)this.ItemFrameControl).NameFrame; } }

        public PickableFrame ProgressFrame { get { return ((ExperienceItemFrameControl)this.ItemFrameControl).RHoldFrame; } }

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
                return this.Item[ItemState.Item_Is_Editing]() || 
                       this.Item[ItemState.Item_Is_Pending]();
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // Mouse and keyboard in modifier
            base.UpdateInput(input, time);

            // Keyboard
            if (this.ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    // in pending status
                    if (this.Item[ItemState.Item_Is_Pending]())
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
                            this.View[ViewState.View_Is_Editing] = ()=> false;
                            this.Item[ItemState.Item_Is_Pending] = ()=> false;
                        }
                    }

                    // Enter synchronization when
                    // Accepting input but not locked
                    if (!this.Locked)
                    {
                        // Normal status
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TaskEditItem))
                        {
                            this.View[ViewState.View_Is_Editing] = ()=> true;
                            this.Item[ItemState.Item_Is_Pending] = ()=> true;
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