namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public class ViewItem1DLogicControl : ViewItemComponent
    {
        private bool isFrameInitialized;

        #region Constructors

        public ViewItem1DLogicControl(IViewItem item)
            : base(item)
        {
            ?? DO DEPENDENCY INJECTION HERE
            this.ItemFrameControl = new ViewItemFrameControl(item);
            this.ItemViewControl  = new ViewItemView1DControl(item);
            this.ItemDataControl  = new ViewItemDataModifier(item);
        }

        #endregion

        #region Destructors

        ~ViewItem1DLogicControl()
        {
            this.Dispose();
        }

        #endregion

        #region IDisposable

        public override void Dispose()
        {
            if (this.ItemFrameControl != null)
            {
                this.ItemFrameControl.Dispose();
            }

            if (this.ItemDataControl != null)
            {
                this.ItemDataControl.Dispose();
            }

            // Don't set item view control to null
            base.Dispose();
        }

        #endregion 

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return this.ItemFrameControl.RootFrame; }
        }

        protected ViewItemDataModifier ItemDataControl { get; set; }

        protected dynamic ItemFrameControl { get; set; }

        protected dynamic ItemViewControl { get; set; }

        #region Operations

        /// <summary>
        ///     Must only be called by ViewItemViewControl. This method is called both by
        ///     mouse and keyboard selection. In the case of mouse selection, MouseSelectsIt
        ///     method calls selection control to modify existing selection record. This
        ///     effect causes ViewItemViewControl(keyboard uses this route) to update item
        ///     state and call CommonSelectsIt.
        /// </summary>
        /// <remarks>
        ///     This method is unified only when selection control won't modify Item_Is_Selected state.
        /// </remarks>
        public virtual void CommonSelectsIt()
        {
        }

        /// <summary>
        ///     Same as CommonSelectsIt. Only used by ViewItemViewControl. This method is called
        ///     both by mouse and keyboard selection.
        /// </summary>
        /// <remarks>
        ///     This method is unified only when selection control won't modify Item_Is_Selected state.
        /// </remarks>
        public virtual void CommonUnselectsIt()
        {
        }

        /// <summary>
        ///     Only used by root frame event handler, which will cause common select method to be called. as a result unifying the
        ///     mouse and keyboard selection effect.
        /// </summary>
        public void MouseSelectsIt()
        {
            this.ItemViewControl.MouseSelectsIt();
        }

        /// <summary>
        ///     Same as MouseSelectsIt. Only used by root frame event handler, which will cause common unselect method to be called.
        ///     As a result unifying the mouse and keyboard un-selection effect.
        /// </summary>
        public void MouseUnselectsIt()
        {
            this.ItemViewControl.MouseUnselectsIt();
        }

        public virtual void ExchangeIt(IViewItem draggingItem, IView targetView)
        {
            this.ItemViewControl.ExchangeIt(draggingItem, targetView);
        }

        public virtual void SwapIt(IViewItem draggingItem)
        {
            this.ItemViewControl.SwapIt(draggingItem);
        }

        #endregion Operations

        #region Update

        public virtual bool AcceptInput
        {
            get
            {
                return this.Item[ItemState.Item_Is_Active]() &&
                       this.Item[ItemState.Item_Is_Selected]() &&
                      !this.Item[ItemState.Item_Is_Editing]();
            }
        }

        public bool Active
        {
            get { return this.Item[ItemState.Item_Is_Active](); }
        }

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            if (!this.Active)
            {
                return;
            }

            // Mouse
            if (this.ViewSettings.MouseEnabled)
            {
                this.ItemFrameControl.UpdateInput(time);
            }

            // Keyboard
            if (this.ViewSettings.KeyboardEnabled)
            {
                this.ItemDataControl.UpdateInput(input, time);
            }
        }

        public override void Update(GameTime time)
        {
            this.UpdateForView(time);

            // reduce lagging graphics 
            if (!this.isFrameInitialized)
            {
                this.ItemFrameControl.Update(time);
                this.isFrameInitialized = true;
            }

            // to improve performance
            if (this.Active)
            {
                this.ItemFrameControl.Update(time);
                this.ItemDataControl .Update(time);
            }
        }

        public void UpdateForView(GameTime gameTime)
        {
            // view activation is controlled by item view control
            this.ItemViewControl.Update(gameTime);
        }

        #endregion Update
    }
}