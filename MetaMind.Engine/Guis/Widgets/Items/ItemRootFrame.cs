using MetaMind.Engine.Guis.Elements.Frames;
using MetaMind.Engine.Guis.Widgets.ViewItems;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.Items
{
    public interface IItemRootFrame : IDraggableFrame
    {
        void Disable();

        void Enable();
    }

    public class ItemRootFrame : DraggableFrame, IItemRootFrame
    {
        private IViewItem item;

        public ItemRootFrame( IViewItem item )
        {
            this.item = item;

            MouseLeftClicked += SelectItsItem;
            MouseLeftClickedOutside += UnSelectItsItem;
        }

        private void UnSelectItsItem( object sender, FrameEventArgs e )
        {
            item.ItemControl.UnSelectIt();
        }

        public void Disable()
        {
            Disable( FrameState.Frame_Active );
        }

        public void Enable()
        {
            Enable( FrameState.Frame_Active );
        }

        private void SelectItsItem( object sender, FrameEventArgs e )
        {
            item.ItemControl.SelectIt();
        }
    }
}