namespace MetaMind.Engine.Core.Entity.Common
{
    using System;
    using System.Collections.Generic;

    public interface __IMMInputableBase : IMMUpdateable,
        IComparer<IMMInputtable>,
        IComparable<IMMInputtable>
    {
    }

    public interface __IMMInputableOperations : IMMInputtableOperations 
    {
    }

    public interface IMMInputtable : __IMMInputableBase, __IMMInputableOperations
    {
        bool EntityInputtable { get; }

        int EntityInputOrder { get; }

        event EventHandler<EventArgs> EntityInputableChanged;

        event EventHandler<EventArgs> EntityInputOrderChanged;
    }
}
