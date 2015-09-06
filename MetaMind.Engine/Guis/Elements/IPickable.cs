// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPickable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using System;

    /// <summary>
    ///     Interface supporting basic mouse picking operation and selection.
    /// </summary>
    public interface IPickable
    {
        #region Mouse Left Buttons

        event EventHandler<FrameEventArgs> MouseLeftDoubleClicked;

        #endregion

        #region Mouse Right Buttons

        event EventHandler<FrameEventArgs> MouseRightDoubleClicked;

        #endregion

        #region Mouse General

        event EventHandler<FrameEventArgs> MouseDoubleClicked;

        #endregion
    }
}