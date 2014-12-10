namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class MotivationItemControl : ViewItemControl1D
    {
        public MotivationItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new MotivationItemFrameControl(item);
            this.ItemViewControl  = new MotivationItemViewControl(item);

            this.ItemSymbolControl = new MotivationItemSymbolControl(item);
            this.ItemTaskControl   = new MotivationItemTaskControl(item);
        }

        public MotivationItemTaskControl ItemTaskControl { get; set; }

        public IPickableFrame SymbolFrame
        {
            get { return this.ItemFrameControl.SymbolFrame; }
        }

        public IModule Tracer
        {
            get { return this.ItemTaskControl.TaskTracer; }
        }

        private MotivationItemSymbolControl ItemSymbolControl { get; set; }

        #region Operations

        public override void CommonSelectIt()
        {
            this.ItemTaskControl.SelectIt();
        }

        public override void CommonUnselectIt()
        {
            this.ItemTaskControl.UnselectIt();
        }

        public void DeleteIt()
        {
            // remove from gui
            this.View.Items.Remove(this.Item);

            this.View.Control.ItemFactory.RemoveData(this.Item);
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
                    // normal status
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationEditItem))
                    {
                        this.View.Enable(ViewState.Item_Editting);
                        this.Item.Enable(ItemState.Item_Pending);
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationDeleteItem))
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

                        if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.A))
                        {
                            this.ItemDataControl.EditInt("Attraction");
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                        {
                            this.View.Disable(ViewState.Item_Editting);
                            this.Item.Disable(ItemState.Item_Pending);
                        }
                    }

                    this.ItemSymbolControl.UpdateInput(gameTime);
                }

                if (!this.Locked)
                {
                    // should be outside of the accepting input state
                    // task view is paralled with item input
                    this.ItemTaskControl.UpdateInput(gameTime);
                }
            }
        }

        public override void UpdateStructure(GameTime gameTime)
        {
            base                  .UpdateStructure(gameTime);
            this.ItemSymbolControl.UpdateStructure(gameTime);
            this.ItemTaskControl  .UpdateStructure(gameTime);
        }

        #endregion
    }
}