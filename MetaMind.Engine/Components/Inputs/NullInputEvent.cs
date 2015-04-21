namespace MetaMind.Engine.Components.Inputs
{
    using System;

    using Microsoft.Xna.Framework;

    public class NullInputEvent : IInputEvent
    {
        public event EventHandler<CharEnteredEventArgs> CharEntered;

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<MouseEventArgs> MouseDoubleClick;

        public event EventHandler<MouseEventArgs> MouseDown;

        public event EventHandler<MouseEventArgs> MouseHover;

        public event EventHandler<MouseEventArgs> MouseMove;

        public event EventHandler<MouseEventArgs> MouseUp;

        public event EventHandler<MouseEventArgs> MouseWheel;

        public void Initialize()
        {
        }

        public void UpdateInput(GameTime gameTime)
        {
        }
    }
}