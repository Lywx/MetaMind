using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameConsole.KeyboardCapture
{
    internal class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(Keys keyCode)
        {
            KeyCode = keyCode;
        }

        public Keys KeyCode { get; private set; }
    }
}