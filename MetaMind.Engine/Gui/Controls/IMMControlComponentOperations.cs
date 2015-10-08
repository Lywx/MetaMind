namespace MetaMind.Engine.Gui.Controls
{
    public interface IMMControlComponentOperationsInternal
    {
        void Add(IMMControlComponentInternal component);

        void Remove(IMMControlComponentInternal component);

        bool Contains(IMMControlComponentInternal component, bool recursive);
    }

    public interface IMMControlComponentOperations 
    {
        void Add(IMMControlComponent component);

        void Remove(IMMControlComponent component);

        bool Contains(IMMControlComponent component, bool recursive);
    }
}
