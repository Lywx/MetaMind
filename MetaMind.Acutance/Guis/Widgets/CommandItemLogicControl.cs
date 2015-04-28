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
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class CommandItemLogicControl : ViewItem2DLogicControl
    {
        #region Constructors and Destructors

        public CommandItemLogicControl(IViewItem item, List<Command> source )
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
        ~CommandItemLogicControl()
        {
            this.Dispose();
        }

        #endregion Constructors

        public PickableFrame IdFrame { get { return ((KnowledgeItemFrameControl)ItemFrameControl).IdFrame; } }

        public PickableFrame NameFrame { get { return ((KnowledgeItemFrameControl)ItemFrameControl).NameFrame; } }

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

            GameEngine.Event.QueueEvent(knowledgeRetrievedEvent);
        }

        #endregion Operations

        #region Update

        public bool Locked
        {
            get
            {
                return Item[ItemState.Item_Is_Editing]() || 
                       Item[ItemState.Item_Is_Pending]();
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // mouse and keyboard in modifier
            //-----------------------------------------------------------------
            base.UpdateInput(input, time);

            // keyboard
            //-----------------------------------------------------------------
            if (ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.CommandDeleteItem))
                        {
                            this.DeleteIt();
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.CommandOpenItem))
                        {
                            this.RetrieveIt();
                        }
                    }
                }

                // special
                //----------------------------------------------------------------- 
                if (View.Logic.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.CommandClearItem))
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