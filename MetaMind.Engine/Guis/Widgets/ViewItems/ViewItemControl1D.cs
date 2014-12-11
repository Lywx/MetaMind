namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    using Microsoft.Xna.Framework;

    public class ViewItemControl1D : ViewItemComponent
    {
        private bool frameInitialized;

        #region Constructors

        public ViewItemControl1D(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new ViewItemFrameControl(item);
            this.ItemViewControl = new ViewItemViewControl1D(item);
            this.ItemDataControl = new ViewItemDataControl(item);
        }

        #endregion Constructors

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return this.ItemFrameControl.RootFrame; }
        }

        protected ViewItemDataControl ItemDataControl { get; set; }

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
        ///     This method is unified only when selection control won't modify Item_Selected state.
        /// </remarks>
        public virtual void CommonSelectsIt()
        {
        }

        /// <summary>
        ///     Same as CommonSelectsIt. Only used by ViewItemViewControl. This method is called
        ///     both by mouse and keyboard selection.
        /// </summary>
        /// <remarks>
        ///     This method is unified only when selection control won't modify Item_Selected state.
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
                return this.Item.IsEnabled(ItemState.Item_Active) &&
                       this.Item.IsEnabled(ItemState.Item_Selected) &&
                      !this.Item.IsEnabled(ItemState.Item_Editing);
            }
        }

        public bool Active
        {
            get { return this.Item.IsEnabled(ItemState.Item_Active); }
        }

        public virtual void UpdateInput(GameTime gameTime)
        {
            if (!this.Active)
            {
                return;
            }

            // mouse
            //-----------------------------------------------------------------
            if (this.ViewSettings.MouseEnabled)
            {
                this.ItemFrameControl.UpdateInput(gameTime);
            }

            // keyboard
            //-----------------------------------------------------------------
            if (this.ViewSettings.KeyboardEnabled)
            {
                this.ItemDataControl.UpdateInput(gameTime);
            }
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
            this.UpdateStructureForView(gameTime);

            // reduce lagging graphics 
            if (!this.frameInitialized)
            {
                this.ItemFrameControl.UpdateStructure(gameTime);
                this.frameInitialized = true;
            }

            // to improve performance
            if (this.Active)
            {
                this.ItemFrameControl.UpdateStructure(gameTime);
                this.ItemDataControl .UpdateStructure(gameTime);
            }
        }

        public void UpdateStructureForView(GameTime gameTime)
        {
            // view activation is controlled by item view control
            this.ItemViewControl.UpdateStructure(gameTime);
        }

        #endregion Update
    }
}