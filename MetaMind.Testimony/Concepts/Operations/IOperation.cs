namespace MetaMind.Testimony.Concepts.Operations
{
    public interface IOperation : 
        IOperationComputation, 
        IInnerUpdatable
    {
        string Name { get; }

        string Description { get; }
    }
}