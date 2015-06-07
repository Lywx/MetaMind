namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Concepts;
    using Engine;
    using Engine.Components.Events;
    using Engine.Components.Inputs;
    using Engine.Guis.Elements;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Logic;
    using Engine.Services;
    using Events;
    using Sessions;

    public class ModuleItemLogic : PointView2DItemLogic
    {
        #region Constructors

        public ModuleItemLogic(IViewItem item, List<Module> source)
            : base(item)
        {
            this.ItemFrame = new TraceItemFrame(item);
            this.ItemInteraction  = new ViewItemViewSmartControl<ViewItemSmartSwapProcess>(item, source);
            
            this.ItemFileControl  = new ModuleItemFileControl(item);

            this.NameFrame.MouseLeftDoubleClicked += this.RetrieveIt;
        }
        
        #endregion Constructors

        public ModuleItemFileControl ItemFileControl { get; private set; }

        public PickableFrame IdFrame { get { return ((TraceItemFrame)this.ItemFrame).IdFrame; } }

        public PickableFrame NameFrame { get { return ((TraceItemFrame)this.ItemFrame).NameFrame; } }

        public PickableFrame ExperienceFrame { get { return ((TraceItemFrame)this.ItemFrame).ExperienceFrame; } }


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
            
            View.ViewLogic.ItemFactory.RemoveData(this.Item);
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
                return this.Item[ItemState.Item_Is_Editing]() || 
                       this.Item[ItemState.Item_Is_Pending]();
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
                if (View.ViewLogic.AcceptInput)
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