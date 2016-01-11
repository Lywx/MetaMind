namespace MetaMind.Engine.Components
{
    using System;
    using Entities;
    using Entities.Bases;

    public interface IMMInputableComponentBase : IMMDrawableComponent, IDisposable
    {
        
    }

    public interface IMMInputableComponentOperations : IMMInputOperations
    {
    }

    public interface IMMInputableComponent : IMMInputableComponentBase, IMMInputableComponentOperations
    {
        
    }
}
