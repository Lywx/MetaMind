// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMMInputable.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Entities
{
    using System;

    public interface IMMInputable : IMMUpdateable, IMMInputOperations
    {
        bool Inputable { get; }

        int InputOrder { get; }

        event EventHandler<EventArgs> InputableChanged;

        event EventHandler<EventArgs> InputOrderChanged;
    }
}