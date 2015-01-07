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
        #region Constructors

        public CommandItemControl(IViewItem item, List<CommandEntry> source )
            : base(item)
        {
            this.ItemFrameControl = new TraceItemFrameControl(item);
            this.ItemViewControl  = new SmartItemViewControl<SmartItemSwapProcess>(item, source);

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
                (int)SessionEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(ItemData.Path, ItemData.Offset));

            GameEngine.EventManager.QueueEvent(knowledgeRetrievedEvent);
        }

        #endregion


        #region Operations

        /// <summary>
        /// Delete command item from view
        /// </summary>
        /// <remarks>
        /// Delete command item in view won't delete underlying command entry from commandlist.
        /// Only way to validly delete the entry is to delete corresponding module entry.
        /// </remarks>
        private void DenotifyIt()
        {
            View.Items.Remove(Item);
            ItemData.Reset();
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
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CommandDeleteItem))
                        {
                            this.DenotifyIt();
                        }
                    }
                }

                // special
                //----------------------------------------------------------------- 
                if (View.Control.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.CommandClearItem))
                        {
                            this.DenotifyIt();
                        }
                    }
                }
            }
        }

        #endregion Update
    }
}