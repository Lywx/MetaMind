namespace MetaMind.Engine.Components.Inputs
{
    using System;

    public interface IInputEvent : IGameControllableComponent
    {
        /// <summary>
        /// Event raised when a character has been entered.
        /// </summary>
        event EventHandler<CharEnteredEventArgs> CharEntered;

        /// <summary>
        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// Event raised when a key has been released.
        /// </summary>
        event EventHandler<KeyEventArgs> KeyUp;

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
        event EventHandler<MouseEventArgs> MouseHover;

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
        event EventHandler<MouseEventArgs> MouseWheel;
    }
}