// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputable.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine
{
    using System;

    public interface IInputable : IOuterUpdateable, IInputableOperations
    {
        bool Inputable { get; }

        int InputOrder { get; }

        event EventHandler<EventArgs> InputableChanged;

        event EventHandler<EventArgs> InputOrderChanged;
    }
}