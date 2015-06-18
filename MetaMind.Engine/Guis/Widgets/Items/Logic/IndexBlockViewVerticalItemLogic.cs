namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using System;
    using Components.Inputs;
    using Data;
    using Frames;
    using Interactions;
    using Layers;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Services;
    using Views;
    using Views.Scrolls;
    using Views.Settings;

    public class IndexBlockViewVerticalItemLogic : BlockViewVerticalItemLogic, IIndexBlockViewVerticalItemLogic
    {
        #region Index View

        private IView indexedView;

        private readonly IIndexViewComposer indexedViewComposer;

        public IView IndexedView
        {
            get { return this.indexedView; }
        }

        public bool IndexedViewOpened { get; protected set; }

        public Func<Vector2> IndexedViewPosition { get; set; }

        #endregion

        #region View

        private PointViewVerticalSettings viewSettings;

        private IBlockViewVerticalScrollController viewScroll;

        public new IIndexBlockViewVerticalItemLayout ItemLayout
        {
            get { return (IIndexBlockViewVerticalItemLayout)base.ItemLayout; }
        }

        #endregion

        public IndexBlockViewVerticalItemLogic(
            IViewItem item,
            IViewItemFrame itemFrame,
            IViewItemInteraction itemInteraction,
            IViewItemDataModel itemModel,
            IViewItemLayout itemLayout,
            IIndexViewComposer viewComposer)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
            if (viewComposer == null)
            {
                throw new ArgumentNullException("viewComposer");
            }

            this.indexedViewComposer = viewComposer;
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
            // this.IndexedViewRowDisplay = () => viewSettings.ViewRowDisplay + viewScroll.RowOffset - itemLayout.Row - itemLayout.BlockRow;
        }

        public Func<int> IndexedViewRowDisplay { get; set; }

        #endregion

        #region Host View

        public void OpenIndexView()
        {
            if (this.IndexedView == null)
            {
                this.indexedView = this.indexedViewComposer.Construct(this.Item);
                this.indexedViewComposer.Compose(this.indexedView, this.Item.ItemData);

                this.indexedView.LoadContent(this.GameInterop);
            }

            this.IndexedViewOpened = true;
            //var indexViewLayer = this.IndexedView.GetLayer<BlockViewVerticalLayer>();
            //var indexViewSettings = indexViewLayer.ViewSettings;
            //if (indexViewSettings.ViewRowDisplay < 1)
            //{
            //    this.IndexedViewOpened = false;
            //}
        }

        public void CloseIndexView()
        {
            this.IndexedViewOpened = false;
        }

        #endregion

        #region Buffer

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();
        }

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();
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
                        this.OpenIndexView();
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.Left))
                    {
                        this.CloseIndexView();
                    }
                }
            }

            if (this.IndexedViewOpened)
            {
                this.indexedView.UpdateInput(input, time);
            }
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.IndexedViewOpened)
            {
                var indexedViewLayer = this.indexedView.GetLayer<BlockViewVerticalLayer>();
                var indexedViewSettings = indexedViewLayer.ViewSettings;
                indexedViewSettings.ViewPosition = this.IndexedViewPosition();
                //indexedViewSettings.ViewRowDisplay = this.IndexViewRowDisplay();
                //if (indexedViewSettings.ViewRowDisplay < 1)
                //{
                //    this.IndexedViewOpened = false;
                //}

                // To avoid flickering resulted of buffer, these three update 
                // has to be reasonably close

                this.indexedView.UpdateForwardBuffer();
                this.indexedView.Update(time);
                this.indexedView.UpdateBackwardBuffer();
            }
        }

        #endregion
    }
}