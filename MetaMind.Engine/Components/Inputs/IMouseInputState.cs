// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMouseInputState.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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