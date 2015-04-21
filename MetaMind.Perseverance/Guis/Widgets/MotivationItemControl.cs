namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Elements;
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
            this.ItemFrameControl = new MotivationItemFrameControl(item);
            this.ItemViewControl  = new MotivationItemViewControl(item);

            this.ItemTaskControl  = new MotivationItemTaskControl(item);
        }

        public MotivationItemTaskControl ItemTaskControl { get; set; }

        public IPickableFrame SymbolFrame
        {
            get { return this.ItemFrameControl.SymbolFrame; }
        }

        public IModule Tracer
        {
            get { return this.ItemTaskControl.TaskModule; }
        }

        #region Operations

        public override void CommonSelectsIt()
        {
            this.ItemTaskControl.SelectsIt();
        }

        public override void CommonUnselectsIt()
        {
            this.ItemTaskControl.UnselectsIt();
        }

        public void DeleteIt()
        {
            // remove from gui
            this.View.Items.Remove(this.Item);

            this.View.Control.ItemFactory.RemoveData(this.Item);

            Item.Dispose();
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
                    this.ItemTaskControl.UpdateInput(input, time);
                }
            }
        }

        public override void Update(GameTime time)
        {
            base                .Update(time);
            this.ItemTaskControl.Update(time);
        }

        #endregion
    }
}