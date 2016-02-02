namespace MetaMind.Engine.Core.Backend.Input.Mouse
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class MMMouseInput : IMMMouseInput
    {
        #region Platform API

        private const int WheelDelta =
#if WINDOWS

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645617%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
            120;

#elif LINUX

            // http://linux.die.net/man/3/qwheelevent
            120;
#endif

        #endregion

        private MouseState mouseCurrent;

        private MouseState mousePrevious;

        #region Constructors

        internal MMMouseInput()
        {
            
        }

        #endregion

        #region Button States

        public bool IsButtonLeftClicked
            => this.mouseCurrent.LeftButton == ButtonState.Released &&
               this.mousePrevious.LeftButton == ButtonState.Pressed;

        public bool IsButtonRightClicked
            => this.mouseCurrent.RightButton == ButtonState.Released &&
               this.mousePrevious.RightButton == ButtonState.Pressed;

        #endregion 

        #region Wheel States

        public bool IsWheelScrolledDown
            =>
                this.mouseCurrent.ScrollWheelValue
                < this.mousePrevious.ScrollWheelValue;

        public bool IsWheelScrolledUp
            =>
                this.mouseCurrent.ScrollWheelValue
                > this.mousePrevious.ScrollWheelValue;

        public int WheelScroll
        {
            get
            {
                if (this.IsWheelScrolledUp)
                {
                    return (this.mouseCurrent.ScrollWheelValue
                            - this.mousePrevious.ScrollWheelValue) / WheelDelta;
                }

                return
                    -(this.mouseCurrent.ScrollWheelValue
                      - this.mousePrevious.ScrollWheelValue) / WheelDelta;
            }
        }

        #endregion

        #region Position States

        public Point Position => new Point(this.mouseCurrent.X, this.mouseCurrent.Y);

        #endregion

        #region Update

        public void UpdateInput(GameTime time)
        {
            this.mousePrevious = this.mouseCurrent;
            this.mouseCurrent = Mouse.GetState();
        }

        #endregion Update
    }
}
