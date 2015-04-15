namespace MetaMind.Acutance.Guis.Widgets
{
    using System;
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

    public class ModuleItemControl : ViewItemControl2D
    {
        #region Constructors

        public ModuleItemControl(IViewItem item, List<Module> source)
            : base(item)
        {
            this.ItemFrameControl = new TraceItemFrameControl(item);
            this.ItemViewControl  = new ViewItemViewSmartControl<ViewItemSmartSwapProcess>(item, source);
            
            this.ItemFileControl  = new ModuleItemFileControl(item);

            this.NameFrame.MouseLeftDoubleClicked += this.RetrieveIt;
        }
        
        #endregion Constructors

        public ModuleItemFileControl ItemFileControl { get; private set; }

        public ItemDataFrame IdFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemDataFrame NameFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).NameFrame; } }

        public ItemDataFrame ExperienceFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).ExperienceFrame; } }


        #region Events

        private void RetrieveIt(object sender, FrameEventArgs e)
        {
            this.RetrieveIt();
        }

        #endregion

        #region Operations

        public void RetrieveIt()
        {
            var knowledgeRetrievedEvent = new Event(
                (int)SessionEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(ItemData.Path, 0));

            GameEngine.Event.QueueEvent(knowledgeRetrievedEvent);
        }

        public void DeleteIt()
        {
            View.Items.Remove(this.Item);
            
            View.Control.ItemFactory.RemoveData(this.Item);
        }

        public void ResetIt()
        {
            this.ItemData.Reset();
        }

        #endregion Operations

        #region Update

        public bool Locked
        {
            get
            {
                return this.Item.IsEnabled(ItemState.Item_Editing) || 
                       this.Item.IsEnabled(ItemState.Item_Pending);
            }
        }

        public override void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            // mouse and keyboard in modifier
            //-----------------------------------------------------------------
            base.UpdateInput(, gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (ViewSettings.KeyboardEnabled)
            {
                if (this.AcceptInput)
                {
                    if (!this.Locked)
                    {
                        if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.ModuleDeleteItem))
                        {
                            this.DeleteIt();
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.ModuleOpenItem))
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
                        if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.ModuleClearItem))
                        {
                            this.DeleteIt();
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(KeyboardActions.ModuleResetItem))
                        {
                            this.ResetIt();
                        }
                    }
                }
            }
        }

        #endregion Update
    }
}