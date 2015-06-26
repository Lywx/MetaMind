namespace MetaMind.Testimony.Concepts.Operations
{
    using Engine;

    public interface IOperation :
        IOperationComputation, 
        IInnerUpdatable
    {
    }

}