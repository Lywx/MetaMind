namespace MetaMind.Engine.Components.Inputs
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

        public Keys KeyCode
        {
            get
            {
                return this.keyCode;
            }
        }
    }
}