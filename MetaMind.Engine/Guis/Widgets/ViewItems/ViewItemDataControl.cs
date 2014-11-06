using System;
using System.Diagnostics;
using System.Reflection;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemDataControl : ViewItemComponent
    {
        private string fieldName;

        public ViewItemDataControl( IViewItem item )
            : base( item )
        {
            LabelModifier = new ViewItemCharModifier( item );
        }

        private IViewItemModifier LabelModifier { get; set; }

        public void Update( GameTime gameTime )
        {
        }

        public void EditString( string targetName )
        {
            fieldName = targetName;
            Item.Enable( ItemState.Item_Editing );
            View.Enable( ViewState.Item_Editting );

            FieldInfo field = ItemData.GetType().GetField( targetName );
            LabelModifier.Initialize( field.GetValue( ItemData ) );
            // every time modification ends, ValueModified event will release all the delegate
            // which makes sure that subscriber will only receive EventArgs during modification
            LabelModifier.ValueModified += RefreshEditing;
            LabelModifier.ModificationEnded += TerminateEditing;
        }

        private void RefreshEditing( object sender, ViewItemDataEventArgs e )
        {
            //make sure name is exactly the same as the displayed name
            FieldInfo field = ItemData.GetType().GetField( fieldName );
            field.SetValue( ItemData, FontManager.GetDisaplayableCharacters( ItemSettings.NameFont, e.NewValue ) );
        }

        private void TerminateEditing( object sender, EventArgs e )
        {
            Item.Disable( ItemState.Item_Editing );
            View.Disable( ViewState.Item_Editting );
        }
    }
}