namespace MetaMind.Engine.Guis.Widgets.Items.Logic
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Items.Data;
    using MetaMind.Engine.Guis.Widgets.Items.Frames;
    using MetaMind.Engine.Guis.Widgets.Items.Interactions;
    using MetaMind.Engine.Guis.Widgets.Items.Layouts;
    using MetaMind.Engine.Guis.Widgets.Views.Layers;
    using MetaMind.Engine.Guis.Widgets.Views.Settings;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItemLogic : ViewItemComponent, IViewItemLogic
    {
        private readonly ViewSettings viewSettings;

        private bool isFrameInitialized;

        #region Constructors

        public ViewItemLogic(IViewItem item, IViewItemFrame itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel)
            : base(item)
        {
            if (itemFrame == null)
            {
                throw new ArgumentNullException("itemFrame");
            }

            if (itemInteraction == null)
            {
                throw new ArgumentNullException("itemInteraction");
            }

            if (itemModel == null)
            {
                throw new ArgumentNullException("itemModel");
            }

            this.ItemFrame       = itemFrame;
            this.ItemInteraction = itemInteraction;
            this.ItemModel       = itemModel;

            var viewLayer = this.ViewGetLayer<ViewLayer>();
            this.viewSettings  = viewLayer.ViewSettings;

            this.PassInLogic();
        }

        #endregion

        #region Destructors

        ~ViewItemLogic()
        {
            this.Dispose();
        }

        #endregion

        #region Dependency

        public IViewItemDataModel ItemModel { get; set; }

        public IViewItemFrame ItemFrame { get; set; }

        public IViewItemInteraction ItemInteraction { get; set; }

        public IViewItemLayout ItemLayout { get; set; }

        #endregion

        #region State Logic

        public void PassInLogic()
        {
            this.Item[ItemState.Item_Is_Inputting] = this.ItemIsInputting;
        }

        protected virtual Func<bool> ItemIsInputting
        {
            get
            {
                return () =>
                    this.Item[ItemState.Item_Is_Active]() && 
                    this.Item[ItemState.Item_Is_Selected]() && 
                   !this.Item[ItemState.Item_Is_Editing]();
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            // Reduce lagging graphics 
            if (!this.isFrameInitialized)
            {
                this.isFrameInitialized = true;

                this.ItemFrame.Update(time);
            }

            // For better performance
            if (this.Item[ItemState.Item_Is_Active]())
            {
                this.ItemFrame.Update(time);
                this.ItemModel.Update(time);
            }
        }

        public void UpdateView(GameTime gameTime)
        {
            // View activation is controlled by item view control
            this.ItemInteraction.Update(gameTime);
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (!this.Item[ItemState.Item_Is_Active]())
            {
                return;
            }

            // Mouse
            if (this.viewSettings.MouseEnabled)
            {
                this.ItemFrame.UpdateInput(input, time);
            }

            // Keyboard
            if (this.viewSettings.KeyboardEnabled)
            {
                this.ItemModel.UpdateInput(input, time);
            }
        }
        #endregion Update

        #region IDisposable

        public override void Dispose()
        {
            if (this.ItemFrame != null)
            {
                this.ItemFrame.Dispose();
            }

            if (this.ItemInteraction != null)
            {
                this.ItemInteraction.Dispose();
            }

            if (this.ItemModel != null)
            {
                this.ItemModel.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}