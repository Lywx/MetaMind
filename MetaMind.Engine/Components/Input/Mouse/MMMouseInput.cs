namespace MetaMind.Engine.Components.Input.Mouse
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class MMMouseInput : IMMMouseInput
    {
        private readonly int WheelDelta =
#if WINDOWS

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645617%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
            120;
#elif LINUX

            // http://linux.die.net/man/3/qwheelevent
            120;
#endif

        private MouseState currentState;

        private MouseState previousState;

        #region Constructors

        public MMMouseInput()
        {
            
        }

        #endregion

        #region Button States

        public bool IsButtonLeftClicked
            => this.currentState.LeftButton == ButtonState.Released &&
               this.previousState.LeftButton == ButtonState.Pressed;

        public bool IsButtonRightClicked
            => this.currentState.RightButton == ButtonState.Released &&
               this.previousState.RightButton == ButtonState.Pressed;

        #endregion 

        #region Wheel States

        public bool IsWheelScrolledDown
            =>
                this.currentState.ScrollWheelValue
                < this.previousState.ScrollWheelValue;

        public bool IsWheelScrolledUp
            =>
                this.currentState.ScrollWheelValue
                > this.previousState.ScrollWheelValue;

        public int WheelRelativeMovement
        {
            get
            {
                if (this.IsWheelScrolledUp)
                {
                    return (this.currentState.ScrollWheelValue
                            - this.previousState.ScrollWheelValue) / WheelDelta;
                }

                return
                    -(this.currentState.ScrollWheelValue
                      - this.previousState.ScrollWheelValue) / WheelDelta;
            }
        }

        #endregion

        #region Position States

        public Point Position => new Point(this.currentState.X, this.currentState.Y);

        #endregion

        #region Update

        public void UpdateInput(GameTime time)
        {
            this.previousState = this.currentState;
            this.currentState = Mouse.GetState();
        }

        #endregion Update
    }
}
