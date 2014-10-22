using System;
using System.Linq;
using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemFrameControl
    {
        ItemRootFrame RootFrame { get; }

        void Update( GameTime gameTime );
    }

    public class ViewItemFrameControl : ViewItemComponent, IViewItemFrameControl
    {
        private Point[] frameSizes = new Point[ ( int ) ItemFrameType.TypeNum ];

        public ViewItemFrameControl( IViewItem item )
            : base( item )
        {
            InitializeFrameSizes();
            InitializeFrames( item );
        }

        public ItemRootFrame RootFrame { get; private set; }

        #region Initializations

        private void InitializeFrames( IViewItem item )
        {
            RootFrame = new ItemRootFrame( item );
        }

        private void InitializeFrameSizes()
        {
            foreach ( var frameName in Enum.GetNames( typeof( ItemFrameType ) ).Except( new[ ] { "TypeNum" } ) )
            {
                var sizeName = frameName + "Size";

                var settingFields = typeof( ItemSettings ).GetFields();
                foreach ( var field in settingFields )
                {
                    if ( field.Name == sizeName )
                    {
                        ItemFrameType frameType;

                        var succeed = Enum.TryParse( frameName, out frameType );
                        if ( succeed )
                            SetFrameSize( frameType, ( Point ) field.GetValue( ItemSettings ) );
                    }
                }
            }
        }

        #endregion Initializations

        #region Update

        public void Update( GameTime gameTime )
        {
            UpdateFrames( gameTime );
            UpdateFrameLogics();
        }

        /// <summary>
        /// Updates the item states related to frames.
        /// </summary>
        /// <remarks>
        /// All the frame states change except event type, which should be implemented in custom frame class,
        /// has to be done here to enforce code readability.
        /// </remarks>
        public void UpdateFrameLogics()
        {
            if ( Item.IsEnabled( ItemState.Item_Active ) && !Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                RootFrame.Enable();
            }
            else if ( !Item.IsEnabled( ItemState.Item_Active ) && !Item.IsEnabled( ItemState.Item_Dragging ) )
            {
                RootFrame.Disable();
            }

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
        }

        public void UpdateFrames( GameTime gameTime )
        {
            if ( !Item.IsEnabled( ItemState.Item_Dragging ) && !Item.IsEnabled( ItemState.Item_Swaping ) )
            {
                RootFrame.Initialize( ViewControl.Scroll.RootCenterPoint( ItemControl.Id ), GetFrameSize( ItemFrameType.RootFrame ) );
            }
            else if ( Item.IsEnabled( ItemState.Item_Swaping ) )
            {
                RootFrame.Initialize( ViewControl.Swap.RootCenterPoint(), GetFrameSize( ItemFrameType.RootFrame ) );
            }
            RootFrame.Update( gameTime );
        }

        #endregion Update

        #region Frame Operation

        private Point GetFrameSize( ItemFrameType type )
        {
            return type.GetFrom( frameSizes );
        }

        private void SetFrameSize( ItemFrameType type, Point size )
        {
            type.SetIn( frameSizes, size );
        }

        #endregion Frame Operation
    }
}