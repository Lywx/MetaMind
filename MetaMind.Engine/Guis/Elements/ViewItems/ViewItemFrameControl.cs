namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using MetaMind.Engine.Guis.Elements.Frames;
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

    public interface IViewItemFrameControl
    {
        void UpdateInput( GameTime gameTime );
        void UpdateStructure( GameTime gameTime );
    }

    public class ViewItemFrameControl : ViewItemComponent, IViewItemFrameControl
    {
        public ViewItemFrameControl( IViewItem item )
            : base( item )
        {
            this.RootFrame = new ItemRootFrame( item ) { Size = this.ItemSettings.RootFrameSize };
        }

        public ItemRootFrame RootFrame { get; private set; }

        #region Update

        public virtual void UpdateInput( GameTime gameTime )
        {
            if ( this.RootFrame.IsEnabled( FrameState.Mouse_Over ) )
            {
                this.Item.Enable( ItemState.Item_Mouse_Over );
            }
            else
            {
                this.Item.Disable( ItemState.Item_Mouse_Over );
            }
            if ( this.RootFrame.IsEnabled( FrameState.Frame_Dragging ) )
            {
                this.Item.Enable( ItemState.Item_Dragging );
            }
            else
            {
                this.Item.Disable( ItemState.Item_Dragging );
            }
            this.RootFrame.UpdateInput( gameTime );
        }

        public virtual void UpdateStructure( GameTime gameTime )
        {
            this.UpdateFrameGeometry();
            this.UpdateFrameLogics();
        }

        /// <summary>
        /// Updates the item states related to frames.
        /// </summary>
        /// <remarks>
        /// All the frame states change except event type, which should be implemented in custom frame class,
        /// has to be done here to enforce code readability.
        /// </remarks>
        protected virtual void UpdateFrameLogics()
        {
            // frame activation 
            if ( this.Item.IsEnabled( ItemState.Item_Active ) &&
                !this.Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                this.RootFrame.Enable();
            }
            else if ( !this.Item.IsEnabled( ItemState.Item_Active ) &&
                      !this.Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                this.RootFrame.Disable();
            }
        }

        protected virtual void UpdateFrameGeometry()
        {
            if ( !this.Item.IsEnabled( ItemState.Item_Dragging ) && 
                 !this.Item.IsEnabled( ItemState.Item_Swaping ) )
            {
                this.RootFrame.Center = this.ViewControl.Scroll.RootCenterPoint( this.ItemControl.Id );
            }
            else if ( this.Item.IsEnabled( ItemState.Item_Swaping ) )
            {
                this.RootFrame.Center = this.ViewControl.Swap.RootCenterPoint();
            }
        }

        #endregion Update
    }
}