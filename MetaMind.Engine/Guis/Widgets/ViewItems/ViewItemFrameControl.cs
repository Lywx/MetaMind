using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
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
            RootFrame = new ItemRootFrame( item ) { Size = ItemSettings.RootFrameSize };
        }

        public ItemRootFrame RootFrame { get; private set; }

        #region Update

        public virtual void UpdateInput( GameTime gameTime )
        {
            if ( RootFrame.IsEnabled( FrameState.Mouse_Over ) )
            {
                Item.Enable( ItemState.Item_Mouse_Over );
            }
            else
            {
                Item.Disable( ItemState.Item_Mouse_Over );
            }
            if ( RootFrame.IsEnabled( FrameState.Frame_Dragging ) )
            {
                Item.Enable( ItemState.Item_Dragging );
            }
            else
            {
                Item.Disable( ItemState.Item_Dragging );
            }
            RootFrame.UpdateInput( gameTime );
        }

        public virtual void UpdateStructure( GameTime gameTime )
        {
            UpdateFrameGeometry();
            UpdateFrameLogics();
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
            if ( Item.IsEnabled( ItemState.Item_Active ) &&
                !Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                RootFrame.Enable();
            }
            else if ( !Item.IsEnabled( ItemState.Item_Active ) &&
                      !Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                RootFrame.Disable();
            }
        }

        protected virtual void UpdateFrameGeometry()
        {
            if ( !Item.IsEnabled( ItemState.Item_Dragging ) && !Item.IsEnabled( ItemState.Item_Swaping ) )
            {
                RootFrame.Center = ViewControl.Scroll.RootCenterPoint( ItemControl.Id );
            }
            else if ( Item.IsEnabled( ItemState.Item_Swaping ) )
            {
                RootFrame.Center = ViewControl.Swap.RootCenterPoint();
            }
        }

        #endregion Update
    }
}