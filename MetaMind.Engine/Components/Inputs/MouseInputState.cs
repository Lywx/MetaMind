// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseInputState.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Inputs
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class MouseInputState : IMouseInputState
    {
        #region Settings

        private const int WheelUnit = 120;

        #endregion Settings

        #region Latch State

        private MouseState currentState;

        private MouseState previousState;

        public MouseState CurrentState => this.currentState;

        public MouseState PreviousState => this.previousState;

        #endregion Latch State

        #region Input State

        public bool IsButtonLeftClicked => this.currentState.LeftButton == ButtonState.Released && 
                                           this.previousState.LeftButton == ButtonState.Pressed;

        public bool IsButtonRightClicked => this.currentState.RightButton == ButtonState.Released && 
                                            this.previousState.RightButton == ButtonState.Pressed;

        public bool IsWheelScrolledDown => this.currentState.ScrollWheelValue < this.previousState.ScrollWheelValue;

        public bool IsWheelScrolledUp => this.currentState.ScrollWheelValue > this.previousState.ScrollWheelValue;

        public Point MouseLocation
        {
            get
            {
                var state = Mouse.GetState();
                return new Point(state.X, state.Y);
            }
        }

        public int WheelRelativeMovement
        {
            get
            {
                if (this.IsWheelScrolledUp)
                {
                    return (this.currentState.ScrollWheelValue - this.previousState.ScrollWheelValue) / WheelUnit;
                }

                return -(this.currentState.ScrollWheelValue - this.previousState.ScrollWheelValue) / WheelUnit;
            }
        }

        #endregion Input State

        #region Update

        public void UpdateInput(GameTime time)
        {
            this.previousState = this.currentState;
            this.currentState = Mouse.GetState();
        }

        #endregion Update
    }
}