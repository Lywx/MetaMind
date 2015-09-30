namespace MetaMind.Engine.Screen
{
    using System.Collections.Generic;

    public interface ILayerManager : ILayerManagerOperations
    {
        MMEntityCollection<IMMLayer> Layers { get; }
    }
}