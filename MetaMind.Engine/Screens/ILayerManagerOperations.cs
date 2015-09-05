namespace MetaMind.Engine.Screens
{
    public interface ILayerManagerOperations
    {
        void Add(IGameLayer layer);

        void Remove(IGameLayer layer);
    }
}