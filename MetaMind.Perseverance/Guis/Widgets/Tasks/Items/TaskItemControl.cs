using MetaMind.Engine.Components.Inputs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;
    using MetaMind.Perseverance.Guis.Modules;

    public class TaskItemControl : ViewItemControl2D
    {
        #region Constructors

        public TaskItemControl(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new TaskItemFrameControl(item);
            this.ItemSyncControl  = new TaskItemSyncControl(item);

            // only add this control when 
            if (item.View.Parent is MotivationTaskTracer)
            {
                this.ItemViewControl = new TaskItemViewControlInMotivation(item);
            }

        }

        public ItemEntryFrame ExperienceFrame { get { return ((TaskItemFrameControl)ItemFrameControl).ExperienceFrame; } }

        public ItemEntryFrame IdFrame { get { return ((TaskItemFrameControl)ItemFrameControl).IdFrame; } }

        public ItemEntryFrame NameFrame { get { return ((TaskItemFrameControl)ItemFrameControl).NameFrame; } }

        public ItemEntryFrame ProgressFrame { get { return ((TaskItemFrameControl)ItemFrameControl).ProgressFrame; } }

        public ItemEntryFrame RationaleFrame { get { return ((TaskItemFrameControl)ItemFrameControl).RationaleFrame; } }

        public TaskItemSyncControl ItemSyncControl { get; private set; }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            // remove from gui
            View.Items.Remove(Item);

            // remove from data source
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
            // mouse
            //-----------------------------------------------------------------
            base.UpdateInput(gameTime);

            // keyboard
            //-----------------------------------------------------------------
            if (this.AcceptInput)
            {
                // normal status
                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskEditItem))
                {
                    View.Enable(ViewState.Item_Editting);
                    Item.Enable(ItemState.Item_Pending);
                }

                if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.TaskDeleteItem))
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

                    if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.D))
                    {
                        this.ItemDataControl.EditInt("Done");
                    }

                    if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.E))
                    {
                        this.ItemDataControl.EditExperience("Experience");
                    }

                    if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.L))
                    {
                        this.ItemDataControl.EditInt("Load");
                    }

                    if (InputSequenceManager.Keyboard.IsKeyTriggered(Keys.R))
                    {
                        this.ItemDataControl.EditInt("RationaleScale");
                    }

                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Escape))
                    {
                        View.Disable(ViewState.Item_Editting);
                        Item.Disable(ItemState.Item_Pending);
                    }
                }

                // enter synchronization when
                // accepting input but not locked
                if (!this.Locked)
                {
                    if (InputSequenceManager.Keyboard.IsActionTriggered(Actions.Enter))
                    {
                        this.ItemSyncControl.SwitchSync();
                    }
                }
            }
        }

        #endregion Update
    }
}