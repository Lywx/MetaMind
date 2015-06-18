namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using Components.Inputs;
    using Data;
    using Frames;
    using Interactions;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Services;

    public class IndexBlockViewVerticalItemLogic : BlockViewVerticalItemLogic, IIndexBlockViewVerticalItemLogic
    {
        private IIndexBlockViewVerticalItemInteraction itemInteraction;

        public IndexBlockViewVerticalItemLogic(
            IViewItem item,
            IViewItemFrame itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel itemModel,
            IViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IIndexBlockViewVerticalItemLayout ItemLayout
        {
            get { return (IIndexBlockViewVerticalItemLayout)base.ItemLayout; }
        }

        public new IIndexBlockViewVerticalItemInteraction ItemInteraction
        {
            get { return (IIndexBlockViewVerticalItemInteraction)base.ItemInteraction; }
        }

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var itemLayer = this.ItemGetLayer<IndexBlockViewVerticalItemLayer>();
            this.itemInteraction = itemLayer.ItemInteraction;
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            base.UpdateInput(input, time);

            if (this.View.ViewSettings.KeyboardEnabled)
            {
                var keyboard = input.State.Keyboard;

                if (this.ItemIsInputting())
                {
                    if (keyboard.IsActionTriggered(KeyboardActions.Right))
                    {
                        this.OpenIndexedView();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.CloseIndexedView();
                    }
                }

                if (!this.Item[ItemState.Item_Is_Locking]())
                {
                    // Extra components
                    this.itemInteraction.UpdateInput(input, time);
                }
            }
        }

        protected override void UpdateWhenUsual(GameTime time)
        {
            this.ItemFrame.Update(time);

            // For better performance
            if (this.Item[ItemState.Item_Is_Active]())
            {
                this.ItemModel.Update(time);
            }
        }

        #endregion

        public void OpenIndexedView()
        {
            this.itemInteraction.OpenIndexedView();
        }

        public void CloseIndexedView()
        {
            this.itemInteraction.CloseIndexedView();
        }

        public void ToggleIndexView()
        {
            if (this.itemInteraction.IndexedViewOpened)
            {
                this.CloseIndexedView();
            }
            else
            {
                this.OpenIndexedView();
            }
        }
    }
}