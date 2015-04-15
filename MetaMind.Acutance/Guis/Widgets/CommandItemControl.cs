namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Events;
    using MetaMind.Acutance.Sessions;
    using MetaMind.Engine;
    using MetaMind.Engine.Components.Events;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class CommandItemControl : ViewItemControl2D
    {
        #region Constructors and Destructors

        public CommandItemControl(IViewItem item, List<Command> source )
            : base(item)
        {
            this.ItemFrameControl = new KnowledgeItemFrameControl(item);
            this.ItemViewControl  = new ViewItemViewSmartControl<ViewItemSmartSwapProcess>(item, source);
            this.ItemDataControl  = new CommandItemDataControl(item);

            this.NameFrame.MouseLeftDoubleClicked += this.RetrieveKnowledge;
        }

        /// <remarks>
        /// Don't need to remove delegate RetrieveIt, for NameFrame may be disposed by
        /// ItemFrameControl.
        /// </remarks>>
        ~CommandItemControl()
        {
            this.Dispose();
        }

        #endregion Constructors

        public ItemDataFrame IdFrame { get { return ((KnowledgeItemFrameControl)ItemFrameControl).IdFrame; } }

        public ItemDataFrame NameFrame { get { return ((KnowledgeItemFrameControl)ItemFrameControl).NameFrame; } }

        #region Events

        private void RetrieveKnowledge(object sender, FrameEventArgs e)
        {
            this.RetrieveIt();
        }

        #endregion

        #region Operations

        /// <summary>
        /// Delete command item from view.
        /// </summary>
        /// <remarks>
        /// Delete command item in view won't delete underlying command entry from commandlist.
        /// Only way to validly delete the entry is to delete corresponding module entry.
        /// </remarks>
        public void DeleteIt()
        {
            View.Items.Remove(Item);

            // reset corresponding command entry to turn to running state
            ItemData.Reset();

            this.Dispose();
        }

        private void RetrieveIt()
        {
            var knowledgeRetrievedEvent = new Event(
                (int)SessionEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(this.ItemData.Path, this.ItemData.Offset));

            GameEngine.Events.QueueEvent(knowledgeRetrievedEvent);
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

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            // mouse and keyboard in modifier
            //-----------------------------------------------------------------
            base.UpdateInput(gameInput, gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (gameInput.Sequence.Keyboard.IsActionTriggered(Actions.CommandDeleteItem))
                        {
                            this.DeleteIt();
                        }

                        if (gameInput.Sequence.Keyboard.IsActionTriggered(Actions.CommandOpenItem))
                        {
                            this.RetrieveIt();
                        }
                    }
                }

                // special
                //----------------------------------------------------------------- 
                if (View.Control.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (gameInput.Sequence.Keyboard.IsActionTriggered(Actions.CommandClearItem))
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