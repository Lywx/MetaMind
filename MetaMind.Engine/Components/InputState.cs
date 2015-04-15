// --------------------------------------------------------------------------------------------------------------------
// <copyright file="State.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using MetaMind.Engine.Components.Inputs;

    using Microsoft.Xna.Framework;

    public interface IInputState
    {
        KeyboardInputState Keyboard { get; }

        MouseInputState Mouse { get; }
    }

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputState : IInputState
    {
        private readonly KeyboardInputState keyboard;

        private readonly MouseInputState mouse;

        public KeyboardInputState Keyboard
        {
            get
            {
                return this.keyboard;
            }
        }

        public MouseInputState Mouse
        {
            get
            {
                return this.mouse;
            }
        }

        #region Singleton

        private static InputState Singleton { get; set; }

        public static InputState GetInstance()
        {
            return Singleton ?? (Singleton = new InputState());
        }

        #endregion Singleton

        #region Constructors

        private InputState()
        {
            this.keyboard = KeyboardInputState.GetInstance();
            this.mouse    = MouseInputState   .GetInstance();
        }

        #endregion Constructors

        #region Update and Draw

        public void UpdateInput(IGameInput gameInput, GameTime gameTime)
        {
            this.mouse   .UpdateInput(gameInput, gameTime);
            this.keyboard.UpdateInput(gameInput, gameTime);
        }

        #endregion Update and Draw
    }
}