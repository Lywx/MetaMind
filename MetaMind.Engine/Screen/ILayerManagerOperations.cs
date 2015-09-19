namespace MetaMind.Engine.Screen
{
    public interface ILayerManagerOperations
    {
        void Add(IGameLayer layer);

        void Remove(IGameLayer layer);
    }
}