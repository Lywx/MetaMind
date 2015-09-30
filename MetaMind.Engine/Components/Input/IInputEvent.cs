namespace MetaMind.Engine.Components.Input
{
    using System;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;

    public interface IInputEvent : IMMInputableComponent
    {
        /// <summary>
        /// Event raised when a character has been entered.
        /// </summary>
        event EventHandler<TextInputEventArgs> CharEntered;

        /// <summary>
        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// Event raised when a key has been released.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Event raised when a char key has been pressed.
        /// </summary>
        event EventHandler<KeyPressEventArgs> KeyPress;

        /// <summary>
        /// Event raised when a mouse button has been double clicked.
        /// </summary>
        event EventHandler<MouseEventArgs> MouseDoubleClick;

        /// <summary>
        /// Event raised when a mouse button is pressed.
        /// </summary>
        event EventHandler<MouseEventArgs> MouseDown;

        /// <summary>
        /// Event raised when the mouse has hovered in the same location for a short period of time.
        /// </summary>
        event EventHandler MouseHover;

        /// <summary>
        /// Event raised when the mouse changes location.
        /// </summary>
        event EventHandler<MouseEventArgs> MouseMove;

        /// <summary>
        /// Event raised when a mouse button is released.
        /// </summary>
        event EventHandler<MouseEventArgs> MouseUp;

        /// <summary>
        /// Event raised when the mouse wheel has been moved.
        /// </summary>
        event EventHandler<MouseEventArgs> MouseScroll;
    }
}