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

        event EventHandler<FrameEventArgs> MousePress;

        event EventHandler<FrameEventArgs> MousePressOut;

        event EventHandler<FrameEventArgs> MouseUp;

        event EventHandler<FrameEventArgs> MouseUpOut;

        #endregion

        #region Mouse Left Buttons

        event EventHandler<FrameEventArgs> MousePressLeft;

        event EventHandler<FrameEventArgs> MousePressOutLeft;

        event EventHandler<FrameEventArgs> MouseUpLeft;

        event EventHandler<FrameEventArgs> MouseUpOutLeft;

        #endregion

        #region Mouse Right Buttons

        event EventHandler<FrameEventArgs> MousePressRight;

        event EventHandler<FrameEventArgs> MousePressOutRight;

        event EventHandler<FrameEventArgs> MouseUpRight;

        event EventHandler<FrameEventArgs> MouseUpOutRight;

        #endregion

        event EventHandler<FrameEventArgs> Move;

        event EventHandler<FrameEventArgs> Resize;
    }
}