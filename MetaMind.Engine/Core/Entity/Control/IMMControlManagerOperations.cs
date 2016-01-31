namespace MetaMind.Engine.Core.Entity.Control
{
    public interface IMMControlManagerOperations
    {
        void Add(IMMControlComponentInternal component);

        void Remove(IMMControlComponent component);
    }
}