using System;
using System.Diagnostics;
using System.Reflection;
using MetaMind.Engine.Concepts;
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
            CharModifier = new ViewItemCharModifier( item );
        }

        private IViewItemCharModifier CharModifier { get; set; }

        public void EditInt( string targetName )
        {
            EditStart( targetName );

            CharModifier.ValueModified     += RefreshEditingInt;
            CharModifier.ModificationEnded += TerminateEditing;
        }

        public void EditExperience( string targetName )
        {
            EditStart( targetName );

            CharModifier.ValueModified     += RefreshEditingExperience;
            CharModifier.ModificationEnded += TerminateEditing;
        }
        public void EditString( string targetName )
        {
            EditStart( targetName );

            CharModifier.ValueModified     += RefreshEditingString;
            CharModifier.ModificationEnded += TerminateEditing;
        }

        public void Update( GameTime gameTime )
        {
        }
        private void EditStart( string targetName )
        {
            fieldName = targetName;

            Item.Disable( ItemState.Item_Pending );
            Item.Enable( ItemState.Item_Editing );
            View.Enable( ViewState.Item_Editting );

            FieldInfo field = ItemData.GetType().GetField( targetName );
            CharModifier.Initialize( field.GetValue( ItemData ).ToString() );
        }

        private void RefreshEditingExperience( object sender, ViewItemDataEventArgs e )
        {
            FieldInfo field       = ItemData.GetType().GetField( fieldName );
            string    inputString = FontManager.GetDisaplayableCharacters( ItemSettings.NameFont, e.NewValue );

            // parse input to experience
            int integer;
            var succeded   = Int32.TryParse( inputString, out integer );
            var experience = new Experience( DateTime.Now, TimeSpan.FromHours( integer ), DateTime.Now );
            field.SetValue( ItemData, succeded ? experience : Experience.Zero );
        }

        private void RefreshEditingInt( object sender, ViewItemDataEventArgs e )
        {
            FieldInfo field       = ItemData.GetType().GetField( fieldName );
            string    inputString = FontManager.GetDisaplayableCharacters( ItemSettings.NameFont, e.NewValue );

            // parse input to int
            int result;
            var succeded = Int32.TryParse( inputString, out result );

            field.SetValue( ItemData, succeded ? result : 0 );
        }
        private void RefreshEditingString( object sender, ViewItemDataEventArgs e )
        {
            FieldInfo field       = ItemData.GetType().GetField( fieldName );
            // make sure name is exactly the same as the displayed name
            string    inputString = FontManager.GetDisaplayableCharacters( ItemSettings.NameFont, e.NewValue );

            field.SetValue( ItemData, inputString );
        }

        private void TerminateEditing( object sender, EventArgs e )
        {
            Item.Disable( ItemState.Item_Editing );
            View.Disable( ViewState.Item_Editting );
        }
    }
}