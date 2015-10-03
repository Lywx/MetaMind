namespace MetaMind.Engine.Gui.Renders
{
    public interface IMMRenderComponentOperations
    {
        void Add(IMMRenderComponent component);

        void Remove(IMMRenderComponent component);

        bool Contains(IMMRenderComponent component, bool recursive);
    }
}
