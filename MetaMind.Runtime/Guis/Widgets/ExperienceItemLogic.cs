namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class ExperienceItemLogic : ViewItemLogic
    {
        public ExperienceItemLogic(IViewItem item, dynamic itemFrameControl, dynamic itemViewControl, dynamic itemDataControl)
            : base(item, (object)itemFrameControl, (object)itemViewControl, (object)itemDataControl)
        {
        }

        #region Operations

        public void DeleteIt()
        {
            // remove from gui
            this.View.Items.Remove(this.Item);

            this.View.ViewLogic.ItemFactory.RemoveData(this.Item);

            this.Item.Dispose();
        }

        #endregion

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

            // keyboard
            //-----------------------------------------------------------------
            if (this.ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    // Normal status
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.MotivationEditItem))
                    {
                        this.View[ViewState.View_Is_Editing] = () => true;
                        this.Item[ItemState.Item_Is_Pending]  = () => true;
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.MotivationDeleteItem))
                    {
                        this.DeleteIt();
                    }

                    // Pending status
                    if (this.Item[ItemState.Item_Is_Pending]())
                    {
                        if (input.State.Keyboard.IsKeyTriggered(Keys.N))
                        {
                            this.ItemDataControl.EditString("Name");
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View[ViewState.View_Is_Editing] = () => false;
                            this.Item[ItemState.Item_Is_Pending]  = () => false;
                        }
                    }
                }

                if (!this.Locked)
                {
                    // 
                }
            }
        }

        #endregion
    }
}