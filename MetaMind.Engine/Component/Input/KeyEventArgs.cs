namespace MetaMind.Engine.Component.Input
{
    using System;
    using Microsoft.Xna.Framework.Input;

    public class KeyEventArgs : EventArgs
    {
        private readonly Keys keyCode;

        public KeyEventArgs(Keys keyCode)
        {
            this.keyCode = keyCode;
        }

        public Keys KeyCode => this.keyCode;
    }
}