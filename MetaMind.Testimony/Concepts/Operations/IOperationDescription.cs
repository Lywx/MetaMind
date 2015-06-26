namespace MetaMind.Testimony.Concepts.Operations
{
    using System;
    using Engine;

    public interface IOperationDescription : 
        IOperationDescriptionOrganization,

        IComparable<IOperationDescription>,

        IInnerUpdatable
    {
        string Name { get; }

        string Description { get; }

        string Path { get; }

        IOperation Operation { get; set; }
    }
}