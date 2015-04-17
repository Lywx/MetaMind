// --------------------------------------------------------------------------------------------------------------------
// <copyright file="State.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputState : GameComponent, IInputState
    {
        private readonly KeyboardInputState keyboard;

        private readonly MouseInputState mouse;

        public IKeyboardInputState Keyboard
        {
            get
            {
                return this.keyboard;
            }
        }

        public IMouseInputState Mouse
        {
            get
            {
                return this.mouse;
            }
        }

        #region Singleton

        private static InputState Singleton { get; set; }

        public static InputState GetComponent(GameEngine gameEngine, int updateOrder)
        {
            return Singleton ?? (Singleton = new InputState(gameEngine, updateOrder));
        }

        #endregion Singleton

        #region Constructors

        private InputState(GameEngine gameEngine, int updateOrder)
            : base(gameEngine)
        {
            this.UpdateOrder = updateOrder;

            this.keyboard = KeyboardInputState.GetState();
            this.mouse    = MouseInputState   .GetState();
        }

        #endregion Constructors

        #region Update and Draw

        public void UpdateInput(GameTime gameTime)
        {
            this.mouse   .UpdateInput(gameTime);
            this.keyboard.UpdateInput(gameTime);
        }

        #endregion Update and Draw
    }
}