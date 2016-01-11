namespace MetaMind.Session.Operations
{
    using Engine;

    public interface IOperation :
        IOperationOperations,
        IOperationComputation, 

        IMMFreeUpdatable
    {
    }
}