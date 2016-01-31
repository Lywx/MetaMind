namespace MetaMind.Session.Operations
{
    using Engine;
    using Engine.Core;

    public interface IOperation :
        IOperationOperations,
        IOperationComputation, 

        IMMFreeUpdatable
    {
    }
}