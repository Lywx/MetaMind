namespace MetaMind.Testimony.Concepts.Operations
{
    using System.Collections.Generic;

    public interface IOperationOperations<in TTransition>
    {
        void Accept(TTransition trigger);

        List<IOption> Request();

        void Send();
    }
}