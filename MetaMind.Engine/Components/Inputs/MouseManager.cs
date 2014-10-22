using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Components.Inputs
{
    public class MouseManager
    {
        #region Input Settings

        public static readonly int WheelUnit = 120;

        #endregion Input Settings

        #region Input Data

        private MouseState currentState;
        private MouseState previousState;
        public MouseState CurrentState
        {
            get { return currentState; }
        }
        public MouseState PreviousState
        {
            get { return previousState; }
        }

        #endregion Input Data

        #region Constructors

        private MouseManager()
        {
        }

        public void Initialize()
        {
        }

        #endregion Constructors

        #region Input Triggers

        public bool IsButtonLeftClicked
        {
            get
            {
                if ( currentState.LeftButton == ButtonState.Released &&
                    previousState.LeftButton == ButtonState.Pressed )
                    return true;
                else
                    return false;
            }
        }

        public bool IsButtonRightClicked
        {
            get
            {
                if ( currentState.RightButton == ButtonState.Released &&
                    previousState.RightButton == ButtonState.Pressed )
                    return true;
                else
                    return false;
            }
        }

        public bool IsWheelScrolledDown
        {
            get
            {
                if ( currentState.ScrollWheelValue < previousState.ScrollWheelValue )
                    return true;
                else
                    return false;
            }
        }

        public bool IsWheelScrolledUp
        {
            get
            {
                if ( currentState.ScrollWheelValue > previousState.ScrollWheelValue )
                    return true;
                else
                    return false;
            }
        }

        public int WheelRelativeMovement
        {
            get
            {
                if ( IsWheelScrolledUp )
                    return ( currentState.ScrollWheelValue - previousState.ScrollWheelValue ) / WheelUnit;
                else
                    return -( currentState.ScrollWheelValue - previousState.ScrollWheelValue ) / WheelUnit;
            }
        }

        #endregion Input Triggers

        #region Singleton

        private static MouseManager singleton;

        public static MouseManager GetInstance()
        {
            return singleton ?? ( singleton = new MouseManager() );
        }

        #endregion Singleton

        #region Update

        public void HandleInput()
        {
            previousState = currentState;
            currentState = Mouse.GetState();
        }

        #endregion Update
    }
}