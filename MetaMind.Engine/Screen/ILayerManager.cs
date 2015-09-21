namespace MetaMind.Engine.Screen
{
    using System.Collections.Generic;

    public interface ILayerManager : ILayerManagerOperations
    {
        GameEntityCollection<IGameLayer> Layers { get; }
    }
}