using MetaMind.Engine.Guis.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MetaMind.Engine.Components.Inputs
{
    public class MouseManager : InputObject
    {
        #region Singleton

        private static MouseManager singleton;

        public static MouseManager GetInstance()
        {
            return singleton ?? ( singleton = new MouseManager() );
        }

        #endregion Singleton

        #region Settings

        private const int WheelUnit = 120;

        #endregion Settings

        #region Latch State

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

        #endregion Latch State

        #region Input State

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

        public Point MouseLocation
        {
            get
            {
                var state = Mouse.GetState();
                return new Point( state.X, state.Y );
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

        #endregion Input State

        #region Constructors

        private MouseManager()
        {
        }

        #endregion Constructors

        #region Update

        public override void Draw( GameTime gameTime )
        {
        }

        public override void UpdateInput( GameTime gameTime )
        {
            previousState = currentState;
            currentState = Mouse.GetState();
        }

        public override void UpdateStructure( GameTime gameTime )
        {
        }

        #endregion Update
    }
}