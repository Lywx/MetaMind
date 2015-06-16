namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using System;
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

        private IView indexView;

        private readonly IIndexViewComposer indexViewComposer;

        public IView IndexView
        {
            get { return this.indexView; }
        }

        public bool IndexViewOpened { get; protected set; }

        #endregion

        #region View

        private PointViewVerticalSettings viewSettings;

        private IBlockViewVerticalScrollController viewScroll;

        public new IIndexBlockViewVerticalItemLayout ItemLayout
        {
            get { return (IIndexBlockViewVerticalItemLayout)base.ItemLayout; }
        }

        #endregion

        public IndexBlockViewVerticalItemLogic(IViewItem item, IViewItemFrame itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout, IIndexViewComposer viewComposer)
            : base(item, itemFrame, itemInteraction, itemModel, itemLayout)
        {
            if (viewComposer == null)
            {
                throw new ArgumentNullException("viewComposer");
            }

            this.indexViewComposer = viewComposer;
        }

        #region Layer

        public override void SetupLayer()
        {
            base.SetupLayer();

            var viewLayer = this.ViewGetLayer<BlockViewVerticalLayer>();
            this.viewSettings = viewLayer.ViewSettings;
            this.viewScroll   = viewLayer.ViewScroll;
        }

        #endregion

        #region Host View

        public void OpenIndexView()
        {
            if (this.IndexView == null)
            {
                this.indexView = this.indexViewComposer.Construct(this.Item);
                this.indexViewComposer.Compose(this.indexView, this.Item.ItemData);

                this.indexView.LoadContent(this.GameInterop);
            }

            this.IndexViewOpened = true;
        }

        public void CloseIndexView()
        {
            this.IndexViewOpened = false;
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

            if (this.IndexViewOpened)
            {
                this.indexView.UpdateInput(input, time);
            }
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (this.IndexViewOpened)
            {

                // this.Update

                // To avoid flickering resulted of buffer, these three update 
                // has to be reasonably close
                this.indexView.UpdateForwardBuffer();
                this.indexView.Update(time);
                this.indexView.UpdateBackwardBuffer();
            }
        }

        #endregion
    }
}