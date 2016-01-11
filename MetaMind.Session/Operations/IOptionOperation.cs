namespace MetaMind.Session.Operations
{
    public interface IOptionOperation
    {
        void Accept();

        void Unlock();
    }
}