// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPressable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using System;

    public interface IPressable
    {
        event EventHandler<FrameEventArgs> MouseEnter;

        event EventHandler<FrameEventArgs> MouseLeave;

        event EventHandler<FrameEventArgs> MouseLeftPressed;

        event EventHandler<FrameEventArgs> MouseLeftPressedOutside;

        event EventHandler<FrameEventArgs> MouseLeftReleased;

        event EventHandler<FrameEventArgs> MouseRightPressed;

        event EventHandler<FrameEventArgs> MouseRightPressedOutside;

        event EventHandler<FrameEventArgs> MouseRightReleased;

        event EventHandler<FrameEventArgs> FrameMoved;
    }
}