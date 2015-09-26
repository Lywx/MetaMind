// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPressable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Elements
{
    using System;

    public interface IPressable : IInputable
    {
        #region Mouse General

        event EventHandler<ElementEventArgs> MouseEnter;

        event EventHandler<ElementEventArgs> MouseLeave;

        event EventHandler<ElementEventArgs> MousePress;

        event EventHandler<ElementEventArgs> MousePressOut;

        event EventHandler<ElementEventArgs> MouseUp;

        event EventHandler<ElementEventArgs> MouseUpOut;

        #endregion

        #region Mouse Left Buttons

        event EventHandler<ElementEventArgs> MousePressLeft;

        event EventHandler<ElementEventArgs> MousePressOutLeft;

        event EventHandler<ElementEventArgs> MouseUpLeft;

        event EventHandler<ElementEventArgs> MouseUpOutLeft;

        #endregion

        #region Mouse Right Buttons

        event EventHandler<ElementEventArgs> MousePressRight;

        event EventHandler<ElementEventArgs> MousePressOutRight;

        event EventHandler<ElementEventArgs> MouseUpRight;

        event EventHandler<ElementEventArgs> MouseUpOutRight;

        #endregion
    }
}