namespace MetaMind.Testimony.Concepts.Operations
{
    public interface IOperationOperations
    {
        void Toggle();

        void Lock();

        void Unlock();
    }

    public interface IOperationOperations<in TTransition>
    {
        void Accept(TTransition transition);
    }
}