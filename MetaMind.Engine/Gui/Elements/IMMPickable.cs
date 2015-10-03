// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPickable.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Elements
{
    using System;

    /// <summary>
    ///     Interface supporting basic mouse picking operation and selection.
    /// </summary>
    public interface IMMPickable : IMMPressable
    {
        #region Mouse Left Buttons

        event EventHandler<MMElementEventArgs> MouseDoubleClickLeft;

        #endregion

        #region Mouse Right Buttons

        event EventHandler<MMElementEventArgs> MouseDoubleClickRight;

        #endregion

        #region Mouse General

        event EventHandler<MMElementEventArgs> MouseDoubleClick;

        #endregion
    }
}