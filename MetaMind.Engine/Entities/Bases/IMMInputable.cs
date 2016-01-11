namespace MetaMind.Engine.Entities.Bases
{
    using System;
    using System.Collections.Generic;

    public interface IMMInputableBase : IMMUpdateable,
        IComparer<IMMInputable>,
        IComparable<IMMInputable> {}

    public interface IMMInputableOperations : IMMInputOperations 
    {
    }

    public interface IMMInputable : IMMInputableBase, IMMInputableOperations
    {
        bool EntityInputable { get; }

        int EntityInputOrder { get; }

        event EventHandler<EventArgs> EntityInputableChanged;

        event EventHandler<EventArgs> EntityInputOrderChanged;
    }
}
