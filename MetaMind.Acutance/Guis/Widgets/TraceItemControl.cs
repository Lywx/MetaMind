namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Modules;
    using MetaMind.Perseverance.Guis.Widgets.Tasks.Items;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class TraceItemControl : ViewItemControl2D
    {
        #region Constructors

        public TraceItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new TraceItemFrameControl(item);

            // only add this control when 
            if (item.View.Parent is MotivationTaskTracer)
            {
                this.ItemViewControl = new TaskItemViewControlInMotivation(item);
            }
        }

        public ItemEntryFrame IdFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).NameFrame; } }

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
            // mouse
            //-----------------------------------------------------------------
            base.UpdateInput(gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (this.AcceptInput)
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

                // in pending status
                if (this.Item.IsEnabled(ItemState.Item_Pending))
                {
                    if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.N))
                    {
                        this.ItemDataControl.EditString("Name");
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                    {
                        this.View.Disable(ViewState.Item_Editting);
                        this.Item.Disable(ItemState.Item_Pending);
                    }
                }
            }
        }

        #endregion Update
    }
}