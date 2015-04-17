// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullInputState.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    public class NullInputState : IInputState
    {
        private static readonly KeyboardInputState keyboard = new KeyboardInputState();

        private static readonly MouseInputState mouse = new MouseInputState();

        public IKeyboardInputState Keyboard
        {
            get
            {
                return keyboard;
            }
        }

        public IMouseInputState Mouse
        {
            get
            {
                return mouse;
            }
        }
    }
}