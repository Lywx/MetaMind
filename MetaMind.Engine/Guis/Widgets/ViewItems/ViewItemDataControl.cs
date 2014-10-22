using System;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemDataControl
    {
        void Update( GameTime gameTime );
    }

    public class ViewItemDataControl : ViewItemComponent, IViewItemDataControl
    {
        private IViewItemModifier labelModifier;
        //private ViewItemLabelType 

        public ViewItemDataControl( IViewItem item)
            : base( item )
        {
            labelModifier = new ViewItemCharModifier( item );
        }

        public void EditLabel( ViewItemLabelType type )
        {
            // no need for mouse invocation
            // but do need for keyboard invocation
            Item.Enable( ItemState.Item_Editing );

            labelModifier.Initialize( type.GetLabelFrom( ItemData ) );
            // every time modification ends, ValueModified event will release all the delegate
            // which makes sure that subscriber will only receive EventArgs during modification
            labelModifier.ValueModified += RefreshEditing;
            labelModifier.ModificationEnded += TerminateEditing;
        }

        private void RefreshEditing( object sender, ViewItemDataEventArgs e )
        {
            // make sure name is exactly the same as the displayed name
            //ItemData.Labels = FontManager.GetDisaplayableCharacters( ItemSettings.NameFont, e.NewValue );
            // width contains extra double space margin
            //AdaptWidth();
        }

        private void TerminateEditing( object sender, EventArgs e )
        {
            Item.Disable( ItemState.Item_Editing );
        }

        public void Update( GameTime gameTime )
        {
        }
    }
}