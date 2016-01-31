namespace MetaMind.Engine.Core.Backend
{
    using System;
    using Entity.Common;

    public interface IMMInputtableComponentBase : IMMDrawableComponent, IDisposable
    {
        
    }

    public interface IMMInputtableComponentOperations : IMMUpdateableOperations, IMMInputtableOperations
    {
        
    }

    public interface IMMInputtableComponent : IMMInputtableComponentBase, IMMInputtableComponentOperations
    {
        
    }
}