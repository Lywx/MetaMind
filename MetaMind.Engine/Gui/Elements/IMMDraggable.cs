// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDraggable.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Elements
{
    using System;

    public interface IMMDraggable
    {
        bool Movable { get; }

        event EventHandler<MMElementEventArgs> MouseDrag;

        event EventHandler<MMElementEventArgs> MouseDrop;
    }
}