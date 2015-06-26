namespace MetaMind.Testimony.Concepts.Operations
{
    public interface IOperationComputation
    {
        bool IsOperationActivated { get; }

        bool IsProcedureTransitioning { get; set; }
    }
}