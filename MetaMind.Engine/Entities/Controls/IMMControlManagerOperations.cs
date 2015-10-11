namespace MetaMind.Engine.Entities.Controls
{
    public interface IMMControlManagerOperations
    {
        void Add(IMMControlComponentInternal component);

        void Remove(IMMControlComponent component);
    }
}