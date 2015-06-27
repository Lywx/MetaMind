namespace MetaMind.Testimony.Concepts.Operations
{
    using Engine;

    public interface IOperation :
        IOperationOperations,
        IOperationComputation, 

        IInnerUpdatable
    {
    }
}