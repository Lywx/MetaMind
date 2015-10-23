namespace MetaMind.Engine.Components
{
    using System;
    using Entities;

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
