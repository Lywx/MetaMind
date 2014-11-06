using System;
using MetaMind.Engine.Guis.Elements.Frames;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Items
{
    public interface IItemEntryFrame : IPickableFrame
    {
        void Enable();

        void Disable();
    }

    public class ItemEntryFrame : PickableFrame, IItemEntryFrame
    {
        private IItemObject item;

        public ItemEntryFrame( IItemObject item )
        {
            this.item = item;

            MouseRightClicked += SwitchEditing;
            MouseRightClickedOutside += QuitEditing;
        }

        public void Enable()
        {
            Enable( FrameState.Frame_Active );
        }

        public void Disable()
        {
            Disable( FrameState.Frame_Active );
        }

        private void SwitchEditing( object sender, FrameEventArgs e )
        {
            if ( !item.IsEnabled( ItemState.Item_Active ) )
                return;

            if ( item.IsEnabled( ItemState.Item_Editing ) )
                item.Disable( ItemState.Item_Editing );
            else
                item.Enable( ItemState.Item_Editing );
        }

        private void QuitEditing( object sender, FrameEventArgs e )
        {
            item.Disable( ItemState.Item_Editing );
        }
    }
}