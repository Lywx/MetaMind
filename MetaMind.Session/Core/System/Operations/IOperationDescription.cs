namespace MetaMind.Session.Operations
{
    using System;
    using Engine;
    using Engine.Core;

    public interface IOperationDescription : 
        IOperationDescriptionOrganization,
        IOperationDescriptionOperations,
        IOperationDescriptionComputation,

        IComparable<IOperationDescription>,

        IMMFreeUpdatable
    {
        string Name { get; }

        string Description { get; }

        string Path { get; }
    }
}