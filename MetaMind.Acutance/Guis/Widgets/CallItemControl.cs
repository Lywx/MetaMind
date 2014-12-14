namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class CallItemControl : ViewItemControl2D
    {
        #region Constructors

        public CallItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new TraceItemFrameControl(item);

            this.NameFrame.MouseLeftDoubleClicked += this.RetrieveKnowledge;
        }

        #endregion Constructors

        public ItemEntryFrame IdFrame { get { return ((TraceItemFrameControl)ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((TraceItemFrameControl)ItemFrameControl).NameFrame; } }

        public ItemEntryFrame ExperienceFrame { get { return ((TraceItemFrameControl)ItemFrameControl).ExperienceFrame; } }


        #region Events

        private void RetrieveKnowledge(object sender, FrameEventArgs e)
        {
            var knowledgeRetrievedEvent = new EventBase(
                (int)AdventureEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(ItemData.Path));

            GameEngine.EventManager.QueueEvent(knowledgeRetrievedEvent);
        }

        #endregion


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