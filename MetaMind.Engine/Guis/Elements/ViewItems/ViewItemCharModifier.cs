namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using System;
    using System.Linq;
    using System.Text;

    using MetaMind.Engine.Components;
    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public interface IViewItemCharModifier
    {
        event EventHandler<ViewItemDataEventArgs> ModificationEnded;

        event EventHandler<ViewItemDataEventArgs> ValueModified;

        void Cancel();

        void Draw(GameTime gameTime);

        void Initialize(string oldString);

        void Release();

        void Update(GameTime gameTime);
    }

    public class ViewItemCharModifier : ViewItemComponent, IViewItemCharModifier
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
            this.InputEventManager.CharEntered += this.DetectCharEntered;
            this.InputEventManager.KeyDown += this.DetectEnterKeyDown;
            this.InputEventManager.KeyDown += this.DetectEscapeKeyDown;
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
                this.modificationEnded -= value;
                this.modificationEnded += value;
            }
            remove
            {
                this.modificationEnded -= value;
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

        public void Cancel()
        {
            if ( this.ValueModified != null )
                this.ValueModified( this, new ViewItemDataEventArgs( this.oldString ) );
            if ( this.modificationEnded != null )
                this.modificationEnded( this, new ViewItemDataEventArgs( this.oldString ) );
            // only need to clear ValueModified, assuming ModificationEnded delegates
            // are not needing multiple time operations.
            this.ValueModified = null;
        }

        /// <summary>
        /// Initializes input string.
        /// </summary>
        /// <param name="oldString">The old string.</param>
        public void Initialize( string oldString )
        {
            this.oldString = oldString;
            this.inputString = new StringBuilder( oldString );
        }

        public void Release()
        {
            if ( this.ValueModified != null )
                this.ValueModified( this, new ViewItemDataEventArgs( this.inputString.ToString() ) );
            if ( this.modificationEnded != null )
                this.modificationEnded( this, new ViewItemDataEventArgs( this.inputString.ToString() ) );
            // only need to clear ValueModified, assuming ModificationEnded delegates
            // are not needing multiple time operations.
            this.ValueModified = null;
        }

        private void Modify()
        {
            if ( this.ValueModified != null )
                this.ValueModified( this, new ViewItemDataEventArgs( this.inputString.ToString() ) );
        }

        #endregion Hooks

        #region Event Handlers

        private void DetectEnterKeyDown( object sender, KeyEventArgs e )
        {
            if ( !this.Item.IsEnabled( ItemState.Item_Editing ) )
                return;

            if ( e.KeyCode == Keys.Enter )
                this.Release();
        }

        private void DetectEscapeKeyDown( object sender, KeyEventArgs e )
        {
            if ( !this.Item.IsEnabled( ItemState.Item_Editing ) )
                return;

            // mixed two manager together, might not be good
            if ( e.KeyCode == Keys.Escape ||
               ( this.InputSequenceManager.Keyboard.IsActionTriggered( Actions.Escape ) ) )
                this.Cancel();
        }

        private void HandleBackspace()
        {
            if ( this.inputString.Length > 0 )
            {
                this.inputString.Remove( this.inputString.Length - 1, 1 );
            }
        }

        private void HandleTab()
        {
            this.inputString.Append( "    " );
        }

        /// <summary>
        /// Handles the unprintable and filter out the invalid file characters.
        /// </summary>
        private void HandleUnprintable()
        {
            var invalid = System.IO.Path.GetInvalidFileNameChars();
            var cleaned = new string( this.inputString.ToString().Where( c => !invalid.Contains( c ) ).ToArray() );
            this.inputString = new StringBuilder( cleaned );
        }

        private void DetectCharEntered( object sender, CharacterEventArgs e )
        {
            if ( !this.Item.IsEnabled( ItemState.Item_Editing ) )
                return;

            var newChar = this.IMEEncoding.GetString( e.Character );
            if ( newChar.ToCharArray().Contains( '\b' ) )
                this.HandleBackspace();
            else if ( newChar.ToCharArray().Contains( '\t' ) )
                this.HandleTab();
            else
                this.inputString.Append( newChar );

            this.HandleUnprintable();
            this.Modify();
        }

        #endregion Event Handlers

        #region Update and Draw

        public void Draw( GameTime gameTime )
        {
        }

        public void Update( GameTime gameTime )
        {
        }

        #endregion Update and Draw
    }
}