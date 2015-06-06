namespace MetaMind.Testimony.Guis.Widgets
{
    using Engine.Components.Inputs;
    using Engine.Guis.Widgets.Items;
    using Engine.Guis.Widgets.Items.Data;
    using Engine.Guis.Widgets.Items.Interactions;
    using Engine.Guis.Widgets.Items.Layouts;
    using Engine.Guis.Widgets.Items.Logic;
    using Engine.Guis.Widgets.Views;
    using Engine.Services;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class TestItemLogic : BlockViewVerticalItemLogic
    {
        public TestItemLogic(
            IViewItem            item,
            TestItemFrame        itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel   itemModel,
            IViewItemLayout      itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new TestItemFrame ItemFrame
        {
            get { return (TestItemFrame)base.ItemFrame; }
            set { base.ItemFrame = value; }
        }

        #region Operations

        public void DeleteIt()
        {
            // remove from gui
            this.View.ItemsWrite.Remove(this.Item);

            // TODO:???
            //this.View.ViewLogic.ItemFactory.RemoveData(this.Item);

            this.Item.Dispose();
        }

        #endregion

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
            // Mouse and keyboard in modifier
            base.UpdateInput(input, time);

            // keyboard
            //-----------------------------------------------------------------
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                if (this.ItemIsInputting())
                {
                    // TODO: Delete These
                    // Normal status
                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.MotivationEditItem))
                    {
                        this.View[ViewState.View_Is_Editing] = () => true;
                        this.Item[ItemState.Item_Is_Pending]  = () => true;
                    }

                    if (input.State.Keyboard.IsActionTriggered(KeyboardActions.MotivationDeleteItem))
                    {
                        this.DeleteIt();
                    }

                    // Pending status
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
                }

                if (!this.Locked)
                {
                    // 
                }
            }
        }

        #endregion
    }
}