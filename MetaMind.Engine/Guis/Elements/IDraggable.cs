// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDraggable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Guis.Elements
{
    using System;

    public interface IDraggable
    {
        event EventHandler<FrameEventArgs> MouseDrag;

        event EventHandler<FrameEventArgs> MouseDrop;
    }
}