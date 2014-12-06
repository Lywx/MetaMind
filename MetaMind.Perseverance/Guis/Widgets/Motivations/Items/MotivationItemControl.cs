using MetaMind.Engine.Components.Inputs;
using MetaMind.Engine.Guis.Elements.Frames;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Guis;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;

    public class MotivationItemControl : ViewItemControl1D
    {
        public MotivationItemControl(IViewItem item)
            : base(item)
        {
            ItemFrameControl = new MotivationItemFrameControl(item);
            ItemViewControl  = new MotivationItemViewControl(item);

            ItemSymbolControl = new MotivationItemSymbolControl(item);
            ItemTaskControl   = new MotivationItemTaskControl(item);
        }

        public MotivationItemTaskControl ItemTaskControl { get; set; }

        public IPickableFrame SymbolFrame
        {
            get { return ItemFrameControl.SymbolFrame; }
        }

        public IModule Tracer
        {
            get { return ItemTaskControl.TaskTracer; }
        }

        private MotivationItemSymbolControl ItemSymbolControl { get; set; }

        #region Operations

        public override void CommonSelectIt()
        {
            ItemTaskControl.SelectIt();
        }

        public override void CommonUnselectIt()
        {
            ItemTaskControl.UnselectIt();
        }

        public void DeleteIt()
        {
            // remove from gui
            View.Items.Remove(Item);

            View.Control.ItemFactory.RemoveData(Item);
        }

        #endregion

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
            // mouse
            //-----------------------------------------------------------------
            base.UpdateInput(gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (this.AcceptInput)
            {
                // normal status
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationEditItem))
                {
                    View.Enable(ViewState.Item_Editting);
                    Item.Enable(ItemState.Item_Pending);
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.MotivationDeleteItem))
                {
                    this.DeleteIt();
                }

                // in pending status
                if (Item.IsEnabled(ItemState.Item_Pending))
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
                        View.Disable(ViewState.Item_Editting);
                        Item.Disable(ItemState.Item_Pending);
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

        public override void UpdateStructure(GameTime gameTime)
        {
            base                  .UpdateStructure(gameTime);
            this.ItemSymbolControl.UpdateStructure(gameTime);
            this.ItemTaskControl  .UpdateStructure(gameTime);
        }

        #endregion
    }
}