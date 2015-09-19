// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPickable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Element
{
    using System;

    /// <summary>
    ///     Interface supporting basic mouse picking operation and selection.
    /// </summary>
    public interface IPickable
    {
        #region Mouse Left Buttons

        event EventHandler<ElementEventArgs> MouseDoubleClickLeft;

        #endregion

        #region Mouse Right Buttons

        event EventHandler<ElementEventArgs> MouseDoubleClickRight;

        #endregion

        #region Mouse General

        event EventHandler<ElementEventArgs> MouseDoubleClick;

        #endregion
    }
}