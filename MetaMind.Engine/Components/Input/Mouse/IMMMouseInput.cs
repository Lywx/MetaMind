namespace MetaMind.Engine.Components.Input.Mouse
{
    using Microsoft.Xna.Framework;

    public interface IMMMouseInput
    {
        #region Button States

        bool IsButtonLeftClicked { get; }

        bool IsButtonRightClicked { get; }

        #endregion

        #region Wheel States

        bool IsWheelScrolledDown { get; }

        bool IsWheelScrolledUp { get; }

        /// <summary>
        /// Integer value for wheel scrolling. Positive when scrolled down.
        /// Negative when scrolled down.
        /// </summary>
        int WheelScroll { get; }

        #endregion

        Point Position { get; }
    }
}
