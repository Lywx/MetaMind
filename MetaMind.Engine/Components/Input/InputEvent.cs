// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Event.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Input
{
    using System;
    using System.Windows.Forms;
    using Extensions;
    using Microsoft.Xna.Framework;

    public class InputEvent : MMInputableComponent, IInputEvent
    {
        private Form       windowForm;

        private GameWindow window;

        #region Constructors

        public InputEvent(MMEngine engine) 
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.window = engine.Window;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Event raised when a character has been entered.
        /// </summary>
        public event EventHandler<TextInputEventArgs> CharEntered = delegate { };

        /// <summary>
        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyDown = delegate { };

        /// <summary>
        /// Event raised when a key has been released.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyUp = delegate { };

        public event EventHandler<KeyPressEventArgs> KeyPress = delegate { };

        /// <summary>
        /// Event raised when a mouse button has been double clicked.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDoubleClick = delegate { };

        /// <summary>
        /// Event raised when a mouse button is pressed.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseDown = delegate { };

        /// <summary>
        /// Event raised when the mouse has hovered in the same location for a short period of time.
        /// </summary>
        public event EventHandler MouseHover = delegate { };

        /// <summary>
        /// Event raised when the mouse changes location.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove = delegate { };

        /// <summary>
        /// Event raised when a mouse button is released.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseUp = delegate { };

        /// <summary>
        /// Event raised when the mouse wheel has been moved.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseScroll = delegate { };

        #endregion

        #region Event Registration

        private void RegisterTextInputHandler()
        {
            this.window.TextInput += this.OnCharEntered;
        }

        private void RegisterKeyInputHandlers()
        {
            this.windowForm.KeyUp    += this.OnKeyUp;
            this.windowForm.KeyDown  += this.OnKeyDown;
            this.windowForm.KeyPress += this.OnKeyPress;
        }

        private void RegisterMouseInputHandlers()
        {
            this.windowForm.MouseUp    += this.OnMouseUp;
            this.windowForm.MouseDown  += this.OnMouseDown;

            this.windowForm.MouseHover += this.OnMouseHover;
            this.windowForm.MouseMove  += this.OnMouseMove;
            this.windowForm.MouseWheel += this.OnMouseWheel;

            this.windowForm.MouseDoubleClick += this.OnMouseDoubleClick;
        }

        #endregion

        #region Event On 

        private void OnCharEntered(object sender, TextInputEventArgs args)
        {
            this.CharEntered?.Invoke(sender, args);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs args)
        {
            this.KeyPress?.Invoke(sender, args);
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs args)
        {
            this.KeyDown?.Invoke(sender, new KeyEventArgs(args.KeyCode.Migrate()));
        }

        private void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs args)
        {
            this.KeyUp?.Invoke(sender, new KeyEventArgs(args.KeyCode.Migrate()));
        }

        private void OnMouseWheel(object sender, System.Windows.Forms.MouseEventArgs args)
        {
            this.MouseScroll?.Invoke(sender, args.Migrate());
        }

        private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs args)
        {
            this.MouseMove?.Invoke(sender, args.Migrate());
        }

        private void OnMouseHover(object sender, EventArgs args)
        {
            this.MouseHover?.Invoke(sender, args);
        }

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs args)
        {
            this.MouseDown?.Invoke(sender, args.Migrate());
        }

        private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs args)
        {
            this.MouseUp?.Invoke(sender, args.Migrate());
        }

        private void OnMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs args)
        {
            this.MouseDoubleClick?.Invoke(sender, args.Migrate());
        }

        #endregion

        #region Windows Forms

        /// <summary>
        /// Force application to find current application window controls with System.Windows.Forms (Windows)
        /// </summary>
        /// <remarks>
        /// Has to be called after GraphicsManager initialization, because by then the the windows 
        /// form is constructed.
        /// </remarks>>
        private Form GetWindowForm()
        {
            return (Form)Control.FromHandle(Application.OpenForms[0].Handle);
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            this.windowForm = this.GetWindowForm();

            this.RegisterTextInputHandler();
            this.RegisterKeyInputHandlers();
            this.RegisterMouseInputHandlers();

            base.Initialize();
        }

        #endregion

        #region Update

        public override void UpdateInput(GameTime time)
        {
        }

        #endregion

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        this.DisposeEvents();
                        this.DisposeHandlers();
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void DisposeHandlers()
        {
            this.DisposeTextInputHandler();
            this.DisposeKeyInputHandlers();
            this.DisposeMouseInputHandlers();
        }

        private void DisposeTextInputHandler()
        {
            this.window.TextInput -= this.OnCharEntered;
        }

        private void DisposeKeyInputHandlers()
        {
            this.windowForm.KeyUp    -= this.OnKeyUp;
            this.windowForm.KeyDown  -= this.OnKeyDown;
            this.windowForm.KeyPress -= this.OnKeyPress;
        }

        private void DisposeMouseInputHandlers()
        {
            this.windowForm.MouseUp    -= this.OnMouseUp;
            this.windowForm.MouseDown  -= this.OnMouseDown;

            this.windowForm.MouseHover -= this.OnMouseHover;
            this.windowForm.MouseMove  -= this.OnMouseMove;
            this.windowForm.MouseWheel -= this.OnMouseWheel;

            this.windowForm.MouseDoubleClick -= this.OnMouseDoubleClick;
        }

        private void DisposeEvents()
        {
            this.DisposeTextInputEvent();
            this.DisposeKeyInputEvents();
            this.DisposeMouseInputEvents();
        }

        private void DisposeMouseInputEvents()
        {
            this.MouseUp = null;
            this.MouseDown = null;

            this.MouseDoubleClick = null;

            this.MouseHover = null;
            this.MouseMove = null;
            this.MouseScroll = null;
        }

        private void DisposeKeyInputEvents()
        {
            this.KeyUp = null;
            this.KeyPress = null;
            this.KeyDown = null;
        }

        private void DisposeTextInputEvent()
        {
            this.CharEntered = null;
        }

        #endregion
    }
}