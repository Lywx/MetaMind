using System;
using System.Linq;
using System.Text;
using MetaMind.Engine.Components;
using MetaMind.Engine.Guis.Widgets.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemCharModifier : ViewItemComponent, IViewItemModifier
    {
        #region Input Settings

        private Encoding IMEEncoding = Encoding.GetEncoding( 54936 ); // GB2312-80 for Sougou IME

        #endregion Input Settings

        #region Input Data

        private string oldString = string.Empty;
        private StringBuilder inputString = new StringBuilder();

        #endregion Input Data

        #region Constructors

        public ViewItemCharModifier( IViewItem item )
            : base( item )
        {
            InputEventManager.CharEntered += DetectCharEntered;
            InputEventManager.KeyDown += DetectEnterKeyDown;
            InputEventManager.KeyDown += DetectEscapeKeyDown;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when modification ends as an explicitly implemented event
        /// which reduce duplicate delegates.
        /// </summary>
        public event EventHandler<ViewItemDataEventArgs> ModificationEnded
        {
            add
            {
                // First try to remove the handler, then re-add it
                modificationEnded -= value;
                modificationEnded += value;
            }
            remove
            {
                modificationEnded -= value;
            }
        }

        /// <summary>
        /// Occurs when value is modified.
        /// </summary>
        public event EventHandler<ViewItemDataEventArgs> ValueModified;

        /// <summary>
        /// Occurs when modification ends as a private event, which is explicitly implemented
        /// by ModificationEnded.
        /// </summary>
        private event EventHandler<ViewItemDataEventArgs> modificationEnded;

        #endregion Events

        #region Hooks

        public virtual void Cancel()
        {
            if ( ValueModified != null )
                ValueModified( this, new ViewItemDataEventArgs( oldString ) );
            if ( modificationEnded != null )
                modificationEnded( this, new ViewItemDataEventArgs( oldString ) );
            // only need to clear ValueModified, assuming ModificationEnded delegates
            // are not needing multiple time operations.
            ValueModified = null;
        }

        /// <summary>
        /// Initializes input string.
        /// </summary>
        /// <param name="oldString">The old string.</param>
        public void Initialize( string oldString )
        {
            this.oldString = oldString;
            inputString = new StringBuilder( oldString );
        }

        public virtual void Release()
        {
            if ( ValueModified != null )
                ValueModified( this, new ViewItemDataEventArgs( inputString.ToString() ) );
            if ( modificationEnded != null )
                modificationEnded( this, new ViewItemDataEventArgs( inputString.ToString() ) );
            // only need to clear ValueModified, assuming ModificationEnded delegates
            // are not needing multiple time operations.
            ValueModified = null;
        }

        private void Modify()
        {
            if ( ValueModified != null )
                ValueModified( this, new ViewItemDataEventArgs( inputString.ToString() ) );
        }

        #endregion Hooks

        #region Event Handlers

        private void DetectEnterKeyDown( object sender, KeyEventArgs e )
        {
            if ( !Item.IsEnabled( ItemState.Item_Editing ) )
                return;

            if ( e.KeyCode == Keys.Enter )
                Release();
        }

        private void DetectEscapeKeyDown( object sender, KeyEventArgs e )
        {
            if ( !Item.IsEnabled( ItemState.Item_Editing ) )
                return;

            if ( e.KeyCode == Keys.Escape )
                Cancel();
        }

        private void HandleBackspace()
        {
            if ( inputString.Length > 0 )
            {
                inputString.Remove( inputString.Length - 1, 1 );
            }
        }

        private void HandleTab()
        {
            inputString.Append( "    " );
        }

        /// <summary>
        /// Handles the unprintable and filter out the invalid file characters.
        /// </summary>
        private void HandleUnprintable()
        {
            var invalid = System.IO.Path.GetInvalidFileNameChars();
            var cleaned = new string( inputString.ToString().Where( c => !invalid.Contains( c ) ).ToArray() );
            inputString = new StringBuilder( cleaned );
        }

        private void DetectCharEntered( object sender, CharacterEventArgs e )
        {
            if ( !Item.IsEnabled( ItemState.Item_Editing ) )
                return;

            var newChar = IMEEncoding.GetString( e.Character );
            if ( newChar.ToCharArray().Contains( '\b' ) )
                HandleBackspace();
            else if ( newChar.ToCharArray().Contains( '\t' ) )
                HandleTab();
            else
                inputString.Append( newChar );

            HandleUnprintable();
            Modify();
        }

        #endregion Event Handlers

        #region Update and Draw

        public virtual void Draw( GameTime gameTime )
        {
        }

        public virtual void Update( GameTime gameTime )
        {
        }

        #endregion Update and Draw
    }
}