namespace MetaMind.Acutance.Guis.Widgets
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;
    using MetaMind.Engine.Guis.Widgets.Items.Logic;
    using MetaMind.Engine.Guis.Widgets.Items.Swaps;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class TraceItemLogic : PointView2DItemLogic
    {
        #region Constructors

        public TraceItemLogic(IViewItem item, List<Trace> source)
            : base(item)
        {
            this.ItemFrame = new TraceItemFrame(item);
            this.ItemInteraction  = new ViewItemViewSmartControl<ViewItemSmartSwapProcess>(item, source);
        }

        #endregion Constructors

        #region Operations

        private void DeleteIt()
        {
            View.ItemsWrite.Remove(Item);

            View.ViewLogic.ItemFactory.RemoveData(Item);

            Item.Dispose();
        }

        #endregion Operations

        #region Update

        public Func<bool> ItemIsLocking
        {
            get
            {
                return () => this.Item[ItemState.Item_Is_Editing]() || this.Item[ItemState.Item_Is_Pending]();
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            // Mouse and keyboard in modifier
            base.UpdateInput(input, time);

            // Keyboard
            if (this.viewSettings.KeyboardEnabled)
            {
                if (this.Item[ItemState.Item_Is_Inputting]())
                {
                    // in pending status
                    if (this.Item[ItemState.Item_Is_Pending]())
                    {
                        if (input.State.Keyboard.IsKeyTriggered(Keys.N))
                        {
                            this.ItemModel.EditString("Name");
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View[ViewState.View_Is_Editing] = () => false;
                            this.Item[ItemState.Item_Is_Pending] = () => false;
                        }
                    }

                    if (!this.Item[ItemState.Item_Is_Locking]())
                    {
                        // normal status
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TraceEditItem))
                        {
                            this.View[ViewState.View_Is_Editing] = () => true;
                            this.Item[ItemState.Item_Is_Pending] = () => true;
                        }

                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TraceDeleteItem))
                        {
                            this.DeleteIt();
                        }
                    }
                }

                // Special
                if (this.View[ViewState.View_Is_Inputting]())
                {
                    if (!this.Item[ItemState.Item_Is_Locking]())
                    {
                        if (input.State.Keyboard.IsActionTriggered(KeyboardActions.TraceClearItem))
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