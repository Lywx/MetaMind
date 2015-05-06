// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewItemCharModifier.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Widgets.Items.Data
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using MetaMind.Engine.Components.Inputs;
    using MetaMind.Engine.Events;
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class ViewItemCharModifier : ViewItemComponent, IViewItemCharModifier, IViewItemCharProcessor
    {
        #region Input Settings

        private readonly string cursorSymbol = "][";

        // GB2312-80 for Sougou IME
        private readonly Encoding imeEncoding = Encoding.GetEncoding(54936);

        private readonly char[] invalidChars = Enumerable.Range(0, char.MaxValue + 1)
                                                         .Select(i => (char)i)
                                                         .Where(char.IsControl)
                                                         .ToArray();

        private string cursorCharacter = string.Empty;

        #endregion Input Settings

        #region Input Data

        private StringBuilder currentString = new StringBuilder();
        private int           cursorIndex;
        private string        previousString = string.Empty;

        #endregion Input Data

        #region Dependency

        protected IInputEvent InputEvent { get; private set; }

        #endregion

        #region Constructors

        public ViewItemCharModifier(IViewItem item)
            : base(item)
        {
            this.InputEvent = this.GameInput.Event;

            this.InputEvent.CharEntered += this.DetectCharEntered;
            this.InputEvent.KeyDown     += this.DetectEnterKeyDown;
        }

        #endregion 

        #region Destructors

        ~ViewItemCharModifier()
        {
            this.Dispose();
        }

        #endregion 

        #region IDisposable

        public override void Dispose()
        {
            this.modificationEnded = null;
            this.ValueModified     = null;

            this.InputEvent.CharEntered -= this.DetectCharEntered;
            this.InputEvent.KeyDown     -= this.DetectEnterKeyDown;

            base.Dispose();
        }

        #endregion IDisposable

        #region Events

        private readonly object locker = new object();

        /// <summary>
        /// Occurs when modification ends as an explicitly implemented event
        /// which reduce duplicate delegates.
        /// </summary>
        public event EventHandler<ViewItemDataEventArgs> ModificationEnded
        {
            add
            {
                // Avoid threading problems
                lock (locker)
                {
                    // First try to remove the handler, then re-add it
                    this.modificationEnded -= value;
                    this.modificationEnded += value;
                }
            }

            remove
            {
                lock (locker)
                {
                    this.modificationEnded -= value;
                }
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

        #region Event Handlers

        private void DetectCharEntered(object sender, CharEnteredEventArgs e)
        {
            if (!this.Item[ItemState.Item_Is_Editing]())
            {
                return;
            }

            var newChars = this.imeEncoding.GetString(e.Character);

            // clean index character before processing
            this.RemoveCursor();

            this.HandleControl(newChars);

            this.InsertCursor();

            this.OnValueModified(this.currentString.ToString());
        }

        private void DetectEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (!this.Item[ItemState.Item_Is_Editing]())
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.Release();
            }
        }

        #endregion Event Handlers

        #region State Control

        public void Cancel()
        {
            this.OnValueModified(this.previousString);
            this.OnModificationEnded(this.previousString);

            // only need to clear ValueModified, assuming ModificationEnded delegates
            // are not needing multiple time operations.
            this.ValueModified = null;
        }

        public void Initialize(string prevString, bool showCursor)
        {
            this.cursorCharacter = showCursor ? this.cursorSymbol : string.Empty;

            this.previousString = prevString;

            this.currentString = new StringBuilder(prevString);
            this.cursorIndex = this.currentString.Length;
        }

        public void Release()
        {
            // remove index character after editing
            this.RemoveCursor();

            this.OnValueModified(this.currentString.ToString());
            this.OnModificationEnded(this.currentString.ToString());

            // only need to clear ValueModified, assuming ModificationEnded delegates
            // are not needing multiple time operations.
            this.ValueModified = null;
        }

        private void OnModificationEnded(string str)
        {
            if (this.modificationEnded != null)
            {
                this.modificationEnded(this, new ViewItemDataEventArgs(str));
            }
        }

        private void OnValueModified(string str)
        {
            if (this.ValueModified != null)
            {
                this.ValueModified(this, new ViewItemDataEventArgs(str));
            }
        }

        #endregion State Control

        #region Cursor Operations

        public string RemoveCursor(string dirty)
        {
            if (dirty.Length > 0 &&

                // make sure input characters contain cursor characters
                this.cursorIndex + this.cursorCharacter.Length < dirty.Length + 1 &&
                dirty.Substring(this.cursorIndex, this.cursorCharacter.Length).ToString(CultureInfo.InvariantCulture) == this.cursorCharacter)
            {
                return dirty.Remove(this.cursorIndex, this.cursorCharacter.Length);
            }

            return dirty;
        }

        private void DecrementCursor(int n = 1)
        {
            for (var i = 0; i < n; i++)
            {
                this.cursorIndex--;
            }
        }

        private void IncrementCursor(int n = 1)
        {
            for (var i = 0; i < n; i++)
            {
                this.cursorIndex++;
            }
        }

        private void InsertCursor()
        {
            if (this.cursorIndex < this.currentString.Length + 1)
            {
                this.currentString.Insert(this.cursorIndex, this.cursorCharacter);
            }
        }

        private void RemoveCursor()
        {
            if (this.currentString.Length > 0 &&

                // make sure input characters contain cursor characters
                this.cursorIndex + this.cursorCharacter.Length < this.currentString.Length + 1 &&
                this.currentString.ToString().Substring(this.cursorIndex, this.cursorCharacter.Length).ToString(CultureInfo.InvariantCulture) == this.cursorCharacter)
            {
                this.currentString.Remove(this.cursorIndex, this.cursorCharacter.Length);
            }
        }

        private void MoveCursorLeft()
        {
            if (this.cursorIndex > 0)
            {
                this.RemoveCursor();
                this.DecrementCursor();
                this.InsertCursor();
                this.OnValueModified(this.currentString.ToString());
            }
        }

        private void MoveCursorRight()
        {
            if (this.cursorIndex < this.currentString.Length - this.cursorCharacter.Length)
            {
                this.RemoveCursor();
                this.IncrementCursor();
                this.InsertCursor();
                this.OnValueModified(this.currentString.ToString());
            }
        }

        #endregion 

        #region Keyboard Operations

        private void HandleBackspace()
        {
            if (this.currentString.Length > 0 && this.cursorIndex > 0)
            {
                this.currentString.Remove(this.cursorIndex - 1, 1);
                this.DecrementCursor();
            }
        }

        private void HandleControl(string chars)
        {
            if (chars.ToCharArray().Contains('\b'))
            {
                this.HandleBackspace();
            }
            else if (chars.ToCharArray().Contains('\t'))
            {
                this.HandleTab();
            }
            else
            {
                this.HandleRegular(chars);
            }
        }

        private void HandleDelete()
        {
            if (this.currentString.Length > 0 && this.cursorIndex < this.currentString.Length)
            {
                this.currentString.Remove(this.cursorIndex, 1);
            }
        }

        private void HandleRegular(string chars)
        {
            var previousLength = this.currentString.Length;

            this.currentString.Insert(this.cursorIndex, chars);

            // remove invalid characters
            this.currentString =
                new StringBuilder(
                    new string(this.currentString.ToString().Where(c => !this.invalidChars.Contains(c)).ToArray()));

            // only increment when actually contains printable characters
            if (this.currentString.Length > previousLength)
            {
                this.IncrementCursor();
            }
        }

        private void HandleTab()
        {
            this.currentString.Append("    ");
            this.IncrementCursor(4);
        }

        #endregion

        #region String Operations

        private void DeleteAll()
        {
            this.cursorIndex = 0;
            this.currentString.Clear();
        }

        private void DeleteNextChar()
        {
            this.RemoveCursor();
            this.HandleDelete();
            this.InsertCursor();
            this.OnValueModified(this.currentString.ToString());
        }

        #endregion 

        #region Update and Draw

        public override void UpdateInput(IGameInputService input, GameTime time)
        {
            var keyboard = input.State.Keyboard;

            if (keyboard.IsActionTriggered(KeyboardActions.Escape))
            {
                if (this.Item[ItemState.Item_Is_Editing]())
                {
                    this.Cancel();
                }
            }

            if (keyboard.CtrlDown && keyboard.IsKeyPressed(Keys.Back))
            {
                this.DeleteAll();
            }

            if (this.ComboTriggered(keyboard, Keys.Left))
            {
                this.MoveCursorLeft();
            }

            if (this.ComboTriggered(keyboard, Keys.Right))
            {
                this.MoveCursorRight();
            }

            if (this.ComboTriggered(keyboard, Keys.Delete))
            {
                this.DeleteNextChar();
            }
        }

        private bool ComboTriggered(IKeyboardInputState keyboard, Keys key)
        {
            return (keyboard.IsKeyPressed(key) && keyboard.CtrlDown) || keyboard.IsKeyTriggered(key);
        }

        #endregion Update and Draw
    }
}