namespace MetaMind.Unity.Concepts.Operations
{
    using Engine;

    public interface IOperation :
        IOperationOperations,
        IOperationComputation, 

        IInnerUpdatable
    {
    }
}