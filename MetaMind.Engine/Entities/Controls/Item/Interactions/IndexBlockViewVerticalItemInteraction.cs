namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using System;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Views;
    using Views.Scrolls;

    public class MMIndexBlockViewVerticalItemInteraction : MMBlockViewVerticalItemInteraction, IMMIndexBlockViewVerticalItemInteraction
    {
        private IMMBlockViewVerticalScrollController viewScroll;

        private IMMView indexedView;

        private readonly IIndexViewBuilder indexedViewBuilder;

        public bool IndexedViewOpened => this.IndexedView != null && this.IndexedView[MMViewState.View_Is_Active]();

        public MMIndexBlockViewVerticalItemInteraction(
            IMMViewItem item,
            IMMViewItemLayout itemLayout,
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

        public IMMView IndexedView
        {
            get { return this.indexedView; }
        }

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            var viewLayer = this.GetViewLayer<MMBlockViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;

            var itemLayer = this.GetItemLayer<MMIndexBlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLayout;

            this.IndexedViewPosition = () => this.viewScroll.Position(itemLayout.Row + itemLayout.BlockRow);
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
            if (this.IndexedViewOpened)
            {
                this.IndexedView.UpdateInput(time);
            }

            base.UpdateInput(time);
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
                var indexedViewLayer = this.indexedView.GetLayer<MMBlockViewVerticalLayer>();
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

                this.indexedView.LoadContent();
            }

            this.IndexedView[MMViewState.View_Is_Active] = () => true;
        }

        public void CloseIndexedView()
        {
            this.IndexedView[MMViewState.View_Is_Active] = () => false;
        }

        #endregion
    }
}