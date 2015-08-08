namespace MetaMind.Unity.Concepts.Operations
{
    using System;
    using Engine;

    public interface IOperationDescription : 
        IOperationDescriptionOrganization,
        IOperationDescriptionOperations,
        IOperationDescriptionComputation,

        IComparable<IOperationDescription>,

        IInnerUpdatable
    {
        string Name { get; }

        string Description { get; }

        string Path { get; }
    }
}