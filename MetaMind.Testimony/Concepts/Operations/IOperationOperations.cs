namespace MetaMind.Testimony.Concepts.Operations
{
    public interface IOperationOperations
    {
        void Toggle();

        void LockTransition();

        void UnlockTransition();
    }

    public interface IOperationOperations<in TTransition>
    {
        void Accept(TTransition transition);
    }
}