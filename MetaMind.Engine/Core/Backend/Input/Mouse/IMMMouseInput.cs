namespace MetaMind.Engine.Core.Backend.Input.Mouse
{
    using Entity.Common;
    using Microsoft.Xna.Framework;

    public interface __IMMMouseInputOperations : IMMInputtableOperations
    {
        
    }

    public interface IMMMouseInput : __IMMMouseInputOperations 
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
