namespace MetaMind.Unity.Concepts.Operations
{
    public interface IOptionOperation
    {
        void Accept();

        void Unlock();
    }
}