namespace MetaMind.Engine.Gui.Controls.Item.Logic
{
    using System;
    using Data;
    using Engine.Components.Input;
    using Frames;
    using Interactions;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Service;
    using Views;

    public class ViewItemLogic : ViewItemComponent, IViewItemLogic
    {
        private bool isFrameUpdated;

        private Func<bool> itemIsInputting;

        private Func<bool> itemIsLocking;

        #region Constructors

        public ViewItemLogic(IViewItem item, IViewItemFrameController itemFrame, IViewItemInteraction itemInteraction, IViewItemDataModel itemModel, IViewItemLayout itemLayout)
            : base(item)
        {
            if (itemFrame == null)
            {
                throw new ArgumentNullException(nameof(itemFrame));
            }

            if (itemInteraction == null)
            {
                throw new ArgumentNullException(nameof(itemInteraction));
            }

            if (itemModel == null)
            {
                throw new ArgumentNullException(nameof(itemModel));
            }

            if (itemLayout == null)
            {
                throw new ArgumentNullException(nameof(itemLayout));
            }

            this.ItemFrame       = itemFrame;
            this.ItemInteraction = itemInteraction;
            this.ItemModel       = itemModel;
            this.ItemLayout      = itemLayout;
        }

        #endregion

        #region Destructors

        ~ViewItemLogic()
        {
            this.Dispose();
        }

        #endregion


        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.ItemFrame      ?.Dispose();
                        this.ItemInteraction?.Dispose();
                        this.ItemModel      ?.Dispose();
                        this.ItemLayout     ?.Dispose();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion

        #region Dependency

        public IViewItemDataModel ItemModel { get; set; }

        public IViewItemFrameController ItemFrame { get; set; }

        public IViewItemInteraction ItemInteraction { get; set; }

        public IViewItemLayout ItemLayout { get; set; }

        #endregion

        #region Layer

        public override void Initialize()
        {
            base.Initialize();

            this.SetupLogic();

            this.ItemFrame      .Initialize();
            this.ItemModel      .Initialize();
            this.ItemLayout     .Initialize();
            this.ItemInteraction.Initialize();
        }

        #endregion

        #region State Logic

        private void SetupLogic()
        {
            if (this.ItemIsInputting == null)
            {
                this.ItemIsInputting = 
                    () => this.Item[ViewItemState.Item_Is_Active]() &&
                          this.Item[ViewItemState.Item_Is_Selected]() &&
                         !this.Item[ViewItemState.Item_Is_Editing]();
            }

            if (this.ItemIsLocking == null)
            {
                this.ItemIsLocking = () =>
                    this.Item[ViewItemState.Item_Is_Editing]() || 
                    this.Item[ViewItemState.Item_Is_Pending]();
            }
        }

        public Func<bool> ItemIsInputting
        {
            get
            {
                return this.itemIsInputting;
            }
            set
            {
                this.itemIsInputting = value;
                this.Item[ViewItemState.Item_Is_Inputing] = value;
            }
        }

        public Func<bool> ItemIsLocking
        {
            get { return this.itemIsLocking; }
            set
            {
                this.itemIsLocking = value;
                this.Item[ViewItemState.Item_Is_Locking] = value;
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
            base.Update(time);

            // Reduce lagging graphics 
            if (!this.isFrameUpdated)
            {
                this.isFrameUpdated = true;

                this.UpdateWhenInit(time);
            }
            else
            {
                this.UpdateWhenUsual(time);
            }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (!this.Item[ViewItemState.Item_Is_Active]())
            {
                return;
            }

            // Mouse
            if (this.View.ViewSettings.MouseEnabled)
            {
                this.ItemFrame.UpdateInput(input, time);
            }

            // Keyboard
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                this.ItemModel.UpdateInput(input, time);
            }

            // Keyboard
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                var keyboard = input.State.Keyboard;

                if (this.Item[ViewItemState.Item_Is_Inputing]())
                {
                    if (keyboard.IsActionTriggered(KeyboardActions.CommonEditItem))
                    {
                        this.View[ViewState.View_Is_Editing] = () => true;
                        this.Item[ViewItemState.Item_Is_Pending] = () => true;
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.CommonDeleteItem))
                    {
                        this.DeleteItem();
                    }

                    // Pending status
                    if (this.Item[ViewItemState.Item_Is_Pending]())
                    {
                        if (keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View[ViewState.View_Is_Editing] = () => false;
                            this.Item[ViewItemState.Item_Is_Pending] = () => false;
                        }
                    }
                }

                if (!this.Item[ViewItemState.Item_Is_Locking]())
                {
                    // Extra components
                }
            }
        }

        public void UpdateView(GameTime time)
        {
            this.ItemInteraction.Update(time);
        }

        protected virtual void UpdateWhenInit(GameTime time)
        {
            this.ItemFrame.Update(time);
        }

        protected virtual void UpdateWhenUsual(GameTime time)
        {
            // For better performance
            if (this.Item[ViewItemState.Item_Is_Active]())
            {
                this.ItemFrame.Update(time);
                this.ItemModel.Update(time);
            }
        }

        #endregion Update

        #region Buffer

        public override void UpdateForwardBuffer()
        {
            base.UpdateForwardBuffer();

            // This is order insensitive
            this.ItemLayout     .UpdateForwardBuffer();
            this.ItemFrame      .UpdateForwardBuffer();
            this.ItemModel      .UpdateForwardBuffer();
            this.ItemInteraction.UpdateForwardBuffer();
        }

        public override void UpdateBackwardBuffer()
        {
            base.UpdateBackwardBuffer();

            // This is order insensitive
            this.ItemLayout     .UpdateBackwardBuffer();
            this.ItemFrame      .UpdateBackwardBuffer();
            this.ItemModel      .UpdateBackwardBuffer();
            this.ItemInteraction.UpdateBackwardBuffer();
        }

        #endregion

        #region Operations

        public void DeleteItem()
        {
            this.View.ItemsWrite.Remove(this.Item);

            this.View.ViewLogic.ViewBinding.RemoveData(this.Item);

            this.Item.Dispose();
        }

        #endregion
    }
}