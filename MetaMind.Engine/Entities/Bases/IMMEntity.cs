namespace MetaMind.Engine.Entities.Bases
{
    using System;
    using System.Collections.Generic;
    using Components.Interop.Event;

    public interface IMMEntityBase : IMMUpdateable, IDisposable 
    {
    }

    public interface IMMEntityOperations : IMMInteropOperations 
    {
    }

    public interface IMMEntity : IMMEntityBase, IMMEntityOperations
    {
        Guid EntityGuid { get; }

        List<IMMEventListener> EntityListeners { get; }
    }
}