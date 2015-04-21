// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseInputState.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
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

        public MouseState CurrentState
        {
            get
            {
                return this.currentState;
            }
        }

        public MouseState PreviousState
        {
            get
            {
                return this.previousState;
            }
        }

        #endregion Latch State

        #region Input State

        public bool IsButtonLeftClicked
        {
            get
            {
                if (this.currentState.LeftButton == ButtonState.Released && 
                    this.previousState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsButtonRightClicked
        {
            get
            {
                if (this.currentState.RightButton == ButtonState.Released && 
                    this.previousState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsWheelScrolledDown
        {
            get
            {
                if (this.currentState.ScrollWheelValue < this.previousState.ScrollWheelValue)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsWheelScrolledUp
        {
            get
            {
                if (this.currentState.ScrollWheelValue > this.previousState.ScrollWheelValue)
                {
                    return true;
                }

                return false;
            }
        }

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

        #region Constructors

        public MouseInputState()
        {
        }

        #endregion Constructors

        #region Update

        public void UpdateInput(GameTime gameTime)
        {
            this.previousState = this.currentState;
            this.currentState  = Mouse.GetState();
        }

        #endregion Update
    }
}