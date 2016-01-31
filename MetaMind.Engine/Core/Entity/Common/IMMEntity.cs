namespace MetaMind.Engine.Core.Entity.Common
{
    using System;
    using System.Collections.Generic;
    using Backend.Interop.Event;

    public interface __IMMEntityBase : IMMUpdateable, IDisposable 
    {
    }

    public interface __IMMEntityOperations : IMMLoadableOperations 
    {
    }

    public interface IMMEntity : __IMMEntityBase, __IMMEntityOperations
    {
        Guid EntityGuid { get; }

        List<IMMEventListener> EntityListeners { get; }
    }
}