namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using System;
    using System.Reflection;

    using MetaMind.Engine.Concepts;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Views;

    using Microsoft.Xna.Framework;

    public class ViewItemDataControl : ViewItemComponent
    {
        private string fieldName;

        public ViewItemDataControl( IViewItem item )
            : base( item )
        {
            this.CharModifier = new ViewItemCharModifier( item );
        }

        private IViewItemCharModifier CharModifier { get; set; }

        public void EditInt( string targetName )
        {
            this.EditStart( targetName );

            this.CharModifier.ValueModified     += this.RefreshEditingInt;
            this.CharModifier.ModificationEnded += this.TerminateEditing;
        }

        public void EditExperience( string targetName )
        {
            this.EditStart( targetName );

            this.CharModifier.ValueModified     += this.RefreshEditingExperience;
            this.CharModifier.ModificationEnded += this.TerminateEditing;
        }
        public void EditString( string targetName )
        {
            this.EditStart( targetName );

            this.CharModifier.ValueModified     += this.RefreshEditingString;
            this.CharModifier.ModificationEnded += this.TerminateEditing;
        }

        public void UpdateStructure( GameTime gameTime )
        {
            // TODO: add input allowance structure
        }
        
        private void EditStart( string targetName )
        {
            this.fieldName = targetName;

            this.Item.Disable( ItemState.Item_Pending );
            this.Item.Enable( ItemState.Item_Editing );
            this.View.Enable( ViewState.Item_Editting );

            FieldInfo field = this.ItemData.GetType().GetField( targetName );
            this.CharModifier.Initialize( field.GetValue( this.ItemData ).ToString() );
        }

        private void RefreshEditingExperience( object sender, ViewItemDataEventArgs e )
        {
            FieldInfo field       = this.ItemData.GetType().GetField( this.fieldName );
            string    inputString = this.FontManager.GetDisaplayableCharacters( this.ItemSettings.NameFont, e.NewValue );

            // parse input to experience
            int integer;
            var succeded   = Int32.TryParse( inputString, out integer );
            var experience = new Experience( DateTime.Now, TimeSpan.FromHours( integer ), DateTime.Now );
            field.SetValue( this.ItemData, succeded ? experience : Experience.Zero );
        }

        private void RefreshEditingInt( object sender, ViewItemDataEventArgs e )
        {
            FieldInfo field       = this.ItemData.GetType().GetField( this.fieldName );
            string    inputString = this.FontManager.GetDisaplayableCharacters( this.ItemSettings.NameFont, e.NewValue );

            // parse input to int
            int result;
            var succeded = Int32.TryParse( inputString, out result );

            field.SetValue( this.ItemData, succeded ? result : 0 );
        }
        private void RefreshEditingString( object sender, ViewItemDataEventArgs e )
        {
            FieldInfo field       = this.ItemData.GetType().GetField( this.fieldName );
            // make sure name is exactly the same as the displayed name
            string    inputString = this.FontManager.GetDisaplayableCharacters( this.ItemSettings.NameFont, e.NewValue );

            field.SetValue( this.ItemData, inputString );
        }

        private void TerminateEditing( object sender, EventArgs e )
        {
            this.Item.Disable( ItemState.Item_Editing );
            this.View.Disable( ViewState.Item_Editting );
        }
    }
}