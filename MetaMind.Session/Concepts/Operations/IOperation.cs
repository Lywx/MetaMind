namespace MetaMind.Session.Concepts.Operations
{
    using Engine;

    public interface IOperation :
        IOperationOperations,
        IOperationComputation, 

        IMMFreeUpdatable
    {
    }
}