namespace MetaMind.Acutance.Guis.Widgets
{
    using System.Collections.Generic;
    using System.IO;

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

        public ModuleItemControl(IViewItem item, List<ModuleEntry> source)
            : base(item)
        {
            this.ItemFrameControl = new TraceItemFrameControl(item);
            this.ItemViewControl  = new SmartItemViewControl<SmartItemSwapProcess>(item, source);

            this.NameFrame.MouseLeftDoubleClicked += this.RetrieveIt;
        }

        #endregion Constructors

        public ItemEntryFrame IdFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).NameFrame; } }

        public ItemEntryFrame ExperienceFrame { get { return ((TraceItemFrameControl)this.ItemFrameControl).ExperienceFrame; } }


        #region Events

        private void RetrieveIt(object sender, FrameEventArgs e)
        {
            this.RetrieveIt();
        }

        // TODO: May implement change watcher #1
        // private void RewriteModule(object sender, FileSystemEventArgs e)
        // {
        //    ItemData.Dispose();
        // }

        #endregion

        #region Operations

        // TODO: May implement change watcher #2
        // public void CreateFileWatcher(string folderPath)
        // {
        //     var watcher = new FileSystemWatcher
        //                       {
        //                           Path         = folderPath, 
        //                           NotifyFilter = NotifyFilters.LastWrite,
        //                           Filter       = "*.md",
        //                       };
        //
        //     watcher.Changed += this.RewriteModule;
        //
        //     watcher.EnableRaisingEvents = true;
        // }

        private void RetrieveIt()
        {
            var knowledgeRetrievedEvent = new EventBase(
                (int)SessionEventType.KnowledgeRetrieved,
                new KnowledgeRetrievedEventArgs(this.ItemData.Path, 0));

            GameEngine.EventManager.QueueEvent(knowledgeRetrievedEvent);
        }

        private void DeleteIt()
        {
            View.Items.Remove(this.Item);
            
            View.Control.ItemFactory.RemoveData(this.Item);
        }

        private void ResetIt()
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
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ModuleDeleteItem))
                        {
                            this.DeleteIt();
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ModuleOpenItem))
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
                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ModuleClearItem))
                        {
                            this.DeleteIt();
                        }

                        if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.ModuleResetItem))
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