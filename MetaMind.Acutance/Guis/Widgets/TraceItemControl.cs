namespace MetaMind.Acutance.Guis.Widgets
{
    using System;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class TraceItemControl : ViewItemControl2D
    {
        #region Constructors

        public TraceItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new TraceItemFrameControl(item);
            this.ItemViewControl  = new TraceItemViewControl(item);
        }

        public ItemEntryFrame IdFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).NameFrame; } }

        public ItemEntryFrame ExperienceFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).ExperienceFrame; } }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            View.Items.Remove(Item);

            View.Control.ItemFactory.RemoveData(Item);

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

                    if (!this.Locked)
                    {
                        // normal status
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TraceEditItem))
                        {
                            this.View.Enable(ViewState.Item_Editting);
                            this.Item.Enable(ItemState.Item_Pending);
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TraceDeleteItem))
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
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TraceClearItem))
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