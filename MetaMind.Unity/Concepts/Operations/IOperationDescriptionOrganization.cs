namespace MetaMind.Unity.Concepts.Operations
{
    using System.Collections.Generic;

    public interface IOperationDescriptionOrganization
    {
        List<IOperationDescription> Children { get; }

        IOperationDescription Parent { get; }

        bool HasChildren { get; }

        bool HasParent { get; }
    }
}