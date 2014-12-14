namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class CallItemControl : ViewItemControl2D
    {
        #region Constructors

        public CallItemControl(IViewItem item)
            : base(item)
        {
            ItemFrameControl = new TraceItemFrameControl(item);
            ItemDataControl  = new TraceItemDataControl(item);
        }

        public ItemEntryFrame IdFrame { get { return ((TraceItemFrameControl)ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((TraceItemFrameControl)ItemFrameControl).NameFrame; } }

        public ItemEntryFrame ExperienceFrame { get { return ((TraceItemFrameControl)ItemFrameControl).ExperienceFrame; } }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            View.Items.Remove(Item);

            View.Control.ItemFactory.RemoveData(Item);
        }

        #endregion Operations

        #region Update

        public bool Locked
        {
            get
            {
                return Item.IsEnabled(ItemState.Item_Editing) || 
                       Item.IsEnabled(ItemState.Item_Pending);
            }
        }

        public override void UpdateInput(GameTime gameTime)
        {
            // mouse and keyboard in modifier
            //-----------------------------------------------------------------
            base.UpdateInput(gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CallDeleteItem))
                        {
                            this.DeleteIt();
                        }
                    }
                }

                // special
                //----------------------------------------------------------------- 
                if (View.Control.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CallClearItem))
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