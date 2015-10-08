namespace MetaMind.Engine.Gui.Renders
{
    public interface IMMRenderComponentOperations
    {
        void Add(IMMRenderComponentInternal component);

        void Remove(IMMRenderComponentInternal component);

        bool Contains(IMMRenderComponent component, bool recursive);
    }
}
