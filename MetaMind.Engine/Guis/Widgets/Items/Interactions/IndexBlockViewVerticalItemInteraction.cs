namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using System;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Services;
    using Views;
    using Views.Scrolls;
    using Views.Settings;

    public class IndexBlockViewVerticalItemInteraction : BlockViewVerticalItemInteraction, IIndexBlockViewVerticalItemInteraction
    {
        private PointViewVerticalSettings viewSettings;

        private IBlockViewVerticalScrollController viewScroll;

        private IView indexedView;

        private readonly IIndexViewComposer indexedViewComposer;

        public bool IndexedViewOpened { get; protected set; }

        public IndexBlockViewVerticalItemInteraction(
            IViewItem item,
            IViewItemLayout itemLayout,
            IViewItemLayoutInteraction itemLayoutInteraction,
            IIndexViewComposer viewComposer)
            : base(item, itemLayout, itemLayoutInteraction)
        {
            if (viewComposer == null)
            {
                throw new ArgumentNullException("viewComposer");
            }

            this.indexedViewComposer = viewComposer;
        }

        public Func<Vector2> IndexedViewPosition { get; set; }

        public IView IndexedView
        {
            get { return this.indexedView; }
        }

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<BlockViewVerticalLayer>();
            this.viewScroll = viewLayer.ViewScroll;
            this.viewSettings = viewLayer.ViewSettings;

            var itemLayer = this.ItemGetLayer<IndexBlockViewVerticalItemLayer>();
            var itemLayout = itemLayer.ItemLayout;

            this.IndexedViewPosition = () => viewScroll.Position(itemLayout.Row + itemLayout.BlockRow);
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
                this.indexedView = this.indexedViewComposer.Construct(this.Item);
                this.indexedViewComposer.Compose(this.indexedView, this.Item.ItemData);

                this.indexedView.LoadContent(this.GameInterop);
            }

            this.IndexedViewOpened = true;
        }

        public void CloseIndexedView()
        {
            this.IndexedViewOpened = false;
        }

        #endregion
    }
}