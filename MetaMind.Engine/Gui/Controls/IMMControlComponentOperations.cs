namespace MetaMind.Engine.Gui.Controls
{
    public interface IMMControlComponentOperations 
    {
        void Add(IMMControlComponentInternal component);

        void Remove(IMMControlComponentInternal component);

        bool Contains(IMMControlComponentInternal component, bool recursive);
    }
}
