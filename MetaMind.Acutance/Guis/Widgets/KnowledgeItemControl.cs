namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class KnowledgeItemControl : ViewItemControl2D
    {
        #region Constructors

        public KnowledgeItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new KnowledgeItemFrameControl(item);
            this.ItemDataControl  = new KnowledgeItemDataControl(item);
        }

        public ItemEntryFrame IdFrame { get { return ((KnowledgeItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((KnowledgeItemFrameControl)this.ItemFrameControl).NameFrame; } }

        #endregion Constructors

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
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.KnowledgeEditItem))
                        {
                            this.View.Enable(ViewState.Item_Editting);
                            this.Item.Enable(ItemState.Item_Pending);
                        }
                    }
                }
            }
        }

        #endregion Update

        #region Operations

        public void SetName(string fileName)
        {
            ItemDataControl.EditCancel();

            var itemDataControl = this.ItemDataControl as KnowledgeItemDataControl;
            if (itemDataControl != null)
            {
                itemDataControl.SetName(fileName);
            }
        }

        #endregion
    }
}