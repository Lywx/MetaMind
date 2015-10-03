namespace MetaMind.Engine.Gui.Controls
{
    public interface IMMControlManagerOperations
    {
        void Add(IMMControlComponentInternal component);

        void Remove(IMMControlComponent component);
    }
}