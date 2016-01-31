namespace MetaMind.Engine.Core.Entity.Control.Item.Controllers
{
    using Data;
    using Frames;
    using Interactions;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;

    public class MMIndexBlockViewVerticalItemLogic : MMBlockViewVerticalItemController, IMMIndexBlockViewVerticalItemController
    {
        private IMMIndexBlockViewVerticalItemInteraction itemInteraction;

        public MMIndexBlockViewVerticalItemLogic(
            IMMViewItem item,
            IMMViewItemFrameController itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel itemModel,
            IMMViewItemLayout itemLayout)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
        }

        public new IMMIndexBlockViewVerticalItemLayout ItemLayout => (IMMIndexBlockViewVerticalItemLayout)base.ItemLayout;

        public new IMMIndexBlockViewVerticalItemInteraction ItemInteraction => (IMMIndexBlockViewVerticalItemInteraction)base.ItemInteraction;

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            var itemLayer = this.GetItemLayer<MMIndexBlockViewVerticalItemLayer>();
            this.itemInteraction = itemLayer.ItemInteraction;
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            base.UpdateInput(time);

            if (this.View.ViewSettings.KeyboardEnabled)
            {
                if (!this.Item[MMViewItemState.Item_Is_Locking]())
                {
                    // Extra components
                    this.itemInteraction.UpdateInput(time);
                }
            }
        }

        protected override void UpdateWhenUsual(GameTime time)
        {
            this.ItemFrame.Update(time);

            // For better performance
            if (this.Item[MMViewItemState.Item_Is_Active]())
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