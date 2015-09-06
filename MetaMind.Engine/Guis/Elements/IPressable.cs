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
        #region Mouse General

        event EventHandler<FrameEventArgs> MouseEnter;

        event EventHandler<FrameEventArgs> MouseLeave;

        event EventHandler<FrameEventArgs> MousePressed;

        event EventHandler<FrameEventArgs> MouseReleased;

        event EventHandler<FrameEventArgs> MousePressedOutside;

        #endregion

        #region Mouse Left Buttons

        event EventHandler<FrameEventArgs> MouseLeftPressed;

        event EventHandler<FrameEventArgs> MouseLeftPressedOutside;

        event EventHandler<FrameEventArgs> MouseLeftReleased;

        #endregion

        #region Mouse Right Buttons

        event EventHandler<FrameEventArgs> MouseRightPressed;

        event EventHandler<FrameEventArgs> MouseRightPressedOutside;

        event EventHandler<FrameEventArgs> MouseRightReleased;

        #endregion

        event EventHandler<FrameEventArgs> Moved;

        event EventHandler<FrameEventArgs> Resized;
    }
}