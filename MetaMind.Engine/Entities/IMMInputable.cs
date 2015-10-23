namespace MetaMind.Engine.Entities
{
    using System;
    using System.Collections.Generic;

    public interface IMMInputableBase : IMMUpdateable,
        IComparer<IMMInputable>,
        IComparable<IMMInputable> {}

    public interface IMMInputable : IMMInputableBase, IMMInputOperations
    {
        bool Inputable { get; }

        int InputOrder { get; }

        event EventHandler<EventArgs> InputableChanged;

        event EventHandler<EventArgs> InputOrderChanged;
    }
}
