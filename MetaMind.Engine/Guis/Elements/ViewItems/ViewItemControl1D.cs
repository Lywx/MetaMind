namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Views;

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
        ///     mouse and keyboard selection. In the case of mouse selection, MouseSelectIt
        ///     method calls selection control to modify existing selection record. This
        ///     effect causes ViewItemViewControl(keyboard uses this route) to update item
        ///     state and call CommonSelectIt.
        /// </summary>
        /// <remarks>
        ///     This method is unified only when selection control won't modify Item_Selected state.
        /// </remarks>
        public virtual void CommonSelectIt()
        {
        }

        /// <summary>
        ///     Same as CommonSelectIt. Only used by ViewItemViewControl. This method is called
        ///     both by mouse and keyboard selection.
        /// </summary>
        /// <remarks>
        ///     This method is unified only when selection control won't modify Item_Selected state.
        /// </remarks>
        public virtual void CommonUnselectIt()
        {
        }

        /// <summary>
        ///     Only used by root frame event handler, which will cause common select method to be called. as a result unifying the
        ///     mouse and keyboard selection effect.
        /// </summary>
        public void MouseSelectIt()
        {
            ItemViewControl.MouseSelectIt();
        }

        /// <summary>
        ///     Same as MouseSelectIt. Only used by root frame event handler, which will cause common unselect method to be called.
        ///     As a result unifying the mouse and keyboard un-selection effect.
        /// </summary>
        public void MouseUnselectIt()
        {
            this.ItemViewControl.MouseUnselectIt();
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
                return Item.IsEnabled(ItemState.Item_Active) &&
                       Item.IsEnabled(ItemState.Item_Selected) &&
                      !Item.IsEnabled(ItemState.Item_Editing);
            }
        }

        public bool Active
        {
            get { return Item.IsEnabled(ItemState.Item_Active); }
        }

        public virtual void UpdateInput(GameTime gameTime)
        {
            if (Active)
            {
                // mouse
                this.ItemFrameControl.UpdateInput(gameTime);
            }
        }

        public virtual void UpdateStructure(GameTime gameTime)
        {
            // view activation is controlled by item view control
            this.ItemViewControl.UpdateStructure(gameTime);

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
                this.ItemDataControl.UpdateStructure(gameTime);
            }
        }

        #endregion Update
    }
}