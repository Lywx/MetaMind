// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPressable.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Elements
{
    using System;
    using Entities;

    public interface IMMPressable : IMMInputable
    {
        #region Mouse General

        event EventHandler<MMElementEventArgs> MouseEnter;

        event EventHandler<MMElementEventArgs> MouseLeave;

        event EventHandler<MMElementEventArgs> MousePress;

        event EventHandler<MMElementEventArgs> MousePressOut;

        event EventHandler<MMElementEventArgs> MouseUp;

        event EventHandler<MMElementEventArgs> MouseUpOut;

        #endregion

        #region Mouse Left Buttons

        event EventHandler<MMElementEventArgs> MousePressLeft;

        event EventHandler<MMElementEventArgs> MousePressOutLeft;

        event EventHandler<MMElementEventArgs> MouseUpLeft;

        event EventHandler<MMElementEventArgs> MouseUpOutLeft;

        #endregion

        #region Mouse Right Buttons

        event EventHandler<MMElementEventArgs> MousePressRight;

        event EventHandler<MMElementEventArgs> MousePressOutRight;

        event EventHandler<MMElementEventArgs> MouseUpRight;

        event EventHandler<MMElementEventArgs> MouseUpOutRight;

        #endregion
    }
}