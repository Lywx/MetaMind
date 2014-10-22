using System;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Components.Inputs
{
    public class KeyEventArgs : EventArgs
    {
        private Keys keyCode;

        public KeyEventArgs(Keys keyCode)
        {
            this.keyCode = keyCode;
        }

        public Keys KeyCode
        {
            get { return keyCode; }
        }
    }
}