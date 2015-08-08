namespace MetaMind.Unity.Concepts.Operations
{
    public interface IOperationDescriptionComputation
    {
        IOperation Operation { get; set; }

        string OperationStatus { get; }

        bool IsOperationActivated { get; }

        int ChildrenOperationActivated { get; }
    }
}