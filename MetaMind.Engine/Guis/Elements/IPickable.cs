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
        event EventHandler<FrameEventArgs> MouseLeftDoubleClicked;

        event EventHandler<FrameEventArgs> MouseRightDoubleClicked;
    }
}