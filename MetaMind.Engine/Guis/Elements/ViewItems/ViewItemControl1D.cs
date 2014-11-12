namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

    public class ViewItemControl1D : ViewItemComponent
    {
        protected ViewItemDataControl ItemDataControl { get; set; }

        protected dynamic ItemFrameControl { get; set; }

        protected dynamic ItemViewControl { get; set; }

        #region Constructors

        public ViewItemControl1D(IViewItem item)
            : base(item)
        {
            this.ItemFrameControl = new ViewItemFrameControl(item);
            this.ItemViewControl = new ViewItemViewControl1D(item);
            this.ItemDataControl = new ViewItemDataControl(item);
        }

        #endregion Constructors

        #region Public Properties

        public int Id { get; set; }

        public IItemRootFrame RootFrame
        {
            get { return this.ItemFrameControl.RootFrame; }
        }

        #endregion Public Properties

        #region Operations

        /// <summary>
        /// Only used by root frame event handler, which will cause common
        /// select method to be called. as a result unifying the mouse and
        /// keyboard selection effect.
        /// </summary>
        public void MouseSelectIt()
        {
            ItemViewControl.MouseSelectIt();
        }

        /// <summary>
        /// Only used by root frame event handler, which will cause common
        /// unselect method to be called. as a result unifying the mouse and
        /// keyboard unselection effect.
        /// </summary>
        public void MouseUnselectIt()
        {
            this.ItemViewControl.MouseUnselectIt();
        }

        public virtual void SwapIt(IViewItem draggingItem)
        {
            this.ItemViewControl.SwapIt(draggingItem);
        }

        /// <summary>
        /// Only used by item view control. This method is called both by
        /// mouse and keyboard selection.
        /// </summary>
        public virtual void CommonSelectIt()
        {
        }

        /// <summary>
        /// Only used by item view control. This method is called both by
        /// mouse and keyboard selection.
        /// </summary>
        public virtual void CommonUnselectIt()
        {
        }

        #endregion Operations

        #region Update

        public bool AcceptInput
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
            this.ItemFrameControl.UpdateStructure(gameTime);
            this.ItemDataControl.UpdateStructure(gameTime);
        }

        #endregion Update
    }
}