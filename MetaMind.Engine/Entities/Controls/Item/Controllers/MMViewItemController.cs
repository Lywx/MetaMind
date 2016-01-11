namespace MetaMind.Engine.Entities.Controls.Item.Controllers
{
    using System;
    using Components.Input;
    using Data;
    using Frames;
    using Interactions;
    using Layouts;
    using Microsoft.Xna.Framework;
    using Views;

    public class MMViewItemController : MMViewItemControllerComponent, IMMViewItemController
    {
        private bool isFrameUpdated;

        private Func<bool> itemIsInputting;

        private Func<bool> itemIsLocking;

        #region Constructors and Finalizer

        public MMViewItemController(
            IMMViewItem item,
            IMMViewItemFrameController itemFrame,
            IMMViewItemInteraction itemInteraction,
            IMMViewItemDataModel itemModel,
            IMMViewItemLayout itemLayout)
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

            this.Children.Add(this.ItemFrame);
            this.Children.Add(this.ItemInteraction);
            this.Children.Add(this.ItemModel);
            this.Children.Add(this.ItemLayout);
        }

        ~MMViewItemController()
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

        public IMMViewItemDataModel ItemModel { get; set; }

        public IMMViewItemFrameController ItemFrame { get; set; }

        public IMMViewItemInteraction ItemInteraction { get; set; }

        public IMMViewItemLayout ItemLayout { get; set; }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeLogic();

            this.ItemFrame      .Initialize();
            this.ItemModel      .Initialize();
            this.ItemLayout     .Initialize();
            this.ItemInteraction.Initialize();
        }

        #endregion

        #region State Logic

        private void InitializeLogic()
        {
            if (this.ItemIsInputting == null)
            {
                this.ItemIsInputting = 
                    () => this.Item[MMViewItemState.Item_Is_Active]() &&
                          this.Item[MMViewItemState.Item_Is_Selected]() &&
                          !this.Item[MMViewItemState.Item_Is_Editing]();
            }

            if (this.ItemIsLocking == null)
            {
                this.ItemIsLocking = () =>
                                     this.Item[MMViewItemState.Item_Is_Editing]() || 
                                     this.Item[MMViewItemState.Item_Is_Pending]();
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
                this.Item[MMViewItemState.Item_Is_Inputing] = value;
            }
        }

        public Func<bool> ItemIsLocking
        {
            get { return this.itemIsLocking; }
            set
            {
                this.itemIsLocking = value;
                this.Item[MMViewItemState.Item_Is_Locking] = value;
            }
        }

        #endregion

        #region Update

        public override void Update(GameTime time)
        {
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

        public override void UpdateInput(GameTime time)
        {
            if (!this.Item[MMViewItemState.Item_Is_Active]())
            {
                return;
            }

            // Mouse
            if (this.View.ViewSettings.MouseEnabled)
            {
                this.ItemFrame.UpdateInput(time);
            }

            // Keyboard
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                this.ItemModel.UpdateInput(time);
            }

            // Keyboard
            if (this.View.ViewSettings.KeyboardEnabled)
            {
                var keyboard = this.GlobalInput.State.Keyboard;

                if (this.Item[MMViewItemState.Item_Is_Inputing]())
                {
                    if (keyboard.IsActionTriggered(KeyboardActions.CommonEditItem))
                    {
                        this.View[MMViewState.View_Is_Editing] = () => true;
                        this.Item[MMViewItemState.Item_Is_Pending] = () => true;
                    }

                    if (keyboard.IsActionTriggered(KeyboardActions.CommonDeleteItem))
                    {
                        this.DeleteItem();
                    }

                    // Pending status
                    if (this.Item[MMViewItemState.Item_Is_Pending]())
                    {
                        if (keyboard.IsActionTriggered(KeyboardActions.Escape))
                        {
                            this.View[MMViewState.View_Is_Editing] = () => false;
                            this.Item[MMViewItemState.Item_Is_Pending] = () => false;
                        }
                    }
                }

                if (!this.Item[MMViewItemState.Item_Is_Locking]())
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
            if (this.Item[MMViewItemState.Item_Is_Active]())
            {
                this.ItemFrame.Update(time);
                this.ItemModel.Update(time);
            }
        }

        #endregion Update

        #region Operations

        public void DeleteItem()
        {
            this.ItemsWrite.Remove(this.Item);

            this.View.ViewController.ViewBinding.RemoveData(this.Item);

            this.Item.Dispose();
        }

        #endregion
    }
}