// --------------------------------------------------------------------------------------------------------------------
// <copyright file="d.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    public interface IInputable : IOuterUpdateable, IInputableOperations
    {
        event EventHandler<EventArgs> ControllableChanged;

        event EventHandler<EventArgs> InputOrderChanged;

        int InputOrder { get; }
    }
}