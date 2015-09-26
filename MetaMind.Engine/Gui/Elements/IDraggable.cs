// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDraggable.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Gui.Elements
{
    using System;

    public interface IDraggable
    {
        bool Movable { get; }

        event EventHandler<ElementEventArgs> MouseDrag;

        event EventHandler<ElementEventArgs> MouseDrop;
    }
}