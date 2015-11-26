namespace MetaMind.Engine.Components.Input
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

        int WheelRelativeMovement { get; }

        #endregion

        Point Position { get; }
    }
}
