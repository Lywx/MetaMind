namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class MotivationItemControl : ViewItemControl1D
    {
        public MotivationItemControl(IViewItem item)
            : base(item)
        {
        }

        #region Operations

        public override void CommonSelectsIt()
        {
        }

        public override void CommonUnselectsIt()
        {
        }

        public void DeleteIt()
        {
            // remove from gui
            this.View.Items.Remove(this.Item);

            this.View.Control.ItemFactory.RemoveData(this.Item);

            this.Item.Dispose();
        }

        #endregion

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
                    // normal status
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.MotivationEditItem))
                    {
                        this.View.Enable(ViewState.Item_Editting);
                        this.Item.Enable(ItemState.Item_Pending);
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.MotivationDeleteItem))
                    {
                        this.DeleteIt();
                    }

                    // in pending status
                    if (this.Item.IsEnabled(ItemState.Item_Pending))
                    {
                        if (input.State.Keyboard.IsKeyTriggered(Keys.N))
                        {
                            this.ItemDataControl.EditString("Name");
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View.Disable(ViewState.Item_Editting);
                            this.Item.Disable(ItemState.Item_Pending);
                        }
                    }
                }

                if (!this.Locked)
                {
                    // should be outside of the accepting input state
                    // task view is parallel with item input
                    //this.ItemTaskControl.UpdateInput(input, time);
                }
            }
        }

        #endregion
    }
}