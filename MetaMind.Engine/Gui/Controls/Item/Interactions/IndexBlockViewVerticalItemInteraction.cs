namespace MetaMind.Engine.Gui.Controls.Item.Interactions
{
    using System;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Service;
    using Views;
    using Views.Scrolls;

    public class IndexBlockViewVerticalItemInteraction : BlockViewVerticalItemInteraction, IIndexBlockViewVerticalItemInteraction
    {
        private IBlockViewVerticalScrollController viewScroll;

        private IView indexedView;

        private readonly IIndexViewBuilder indexedViewBuilder;

        public bool IndexedViewOpened => this.IndexedView != null && this.IndexedView[ViewState.View_Is_Active]();

        public IndexBlockViewVerticalItemInteraction(
            IViewItem item,
            IViewItemLayout itemLayout,
            IViewItemLayoutInteraction itemLayoutInteraction,
            IIndexViewBuilder viewBuilder)
            : base(item, itemLayout, itemLayoutInteraction)
        {
            if (viewBuilder == null)
            {
                throw new ArgumentNullException(nameof(viewBuilder));
            }

            this.indexedViewBuilder = viewBuilder;
        }

        public Func<Vector2> IndexedViewPosition { get; set; }

        public IView IndexedView
        {
            get { return this.indexedView; }
        }

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            var viewLayer = this.GetViewLayer<BlockViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;

            var itemLayer = this.GetItemLayer<IndexBlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLayout;

            this.IndexedViewPosition = () => this.viewScroll.Position(itemLayout.Row + itemLayout.BlockRow);
        }

        #endregion

        #region Update

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (this.IndexedViewOpened)
            {
                this.IndexedView.UpdateInput(input, time);
            }

            base.UpdateInput(input, time);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            this.ViewUpdateIndexedView(time);
        }

        public void ViewUpdateIndexedView(GameTime time)
        {
            if (this.IndexedViewOpened)
            {
                var indexedViewLayer = this.indexedView.GetLayer<BlockViewVerticalLayer>();
                var indexedViewSettings = indexedViewLayer.ViewSettings;
                indexedViewSettings.ViewPosition = this.IndexedViewPosition();
                indexedViewSettings.ViewRowDisplay = int.MaxValue;

                // To avoid flickering resulted of buffer, these three update 
                // has to be reasonably close
                this.indexedView.UpdateForwardBuffer();
                this.indexedView.Update(time);
                this.indexedView.UpdateBackwardBuffer();
            }
        }

        #endregion

        #region Operations

        public void OpenIndexedView()
        {
            if (this.IndexedView == null)
            {
                this.indexedView = this.indexedViewBuilder.Clone(this.Item);
                this.indexedViewBuilder.Compose(this.indexedView, this.Item.ItemData);

                this.indexedView.LoadContent(this.Interop);
            }

            this.IndexedView[ViewState.View_Is_Active] = () => true;
        }

        public void CloseIndexedView()
        {
            this.IndexedView[ViewState.View_Is_Active] = () => false;
        }

        #endregion
    }
}