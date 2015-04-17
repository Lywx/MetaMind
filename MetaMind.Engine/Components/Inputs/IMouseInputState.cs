namespace MetaMind.Engine.Components.Inputs
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public interface IMouseInputState
    {
        MouseState CurrentState { get; }

        MouseState PreviousState { get; }

        bool IsButtonLeftClicked { get; }

        bool IsButtonRightClicked { get; }

        bool IsWheelScrolledDown { get; }

        bool IsWheelScrolledUp { get; }

        Point MouseLocation { get; }

        int WheelRelativeMovement { get; }
    }
}