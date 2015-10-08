namespace MetaMind.Session.Concepts.Operations
{
    public interface IOptionOperation
    {
        void Accept();

        void Unlock();
    }
}