namespace MetaMind.Testimony.Concepts.Operations
{
    using System.Collections.Generic;

    public interface IOperationDescriptionOrganization
    {
        List<IOperationDescription> Children { get; }
    }
}