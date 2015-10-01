namespace MetaMind.Engine.Screen
{
    using System.Collections.Generic;
    using Entities;

    public interface ILayerManager : ILayerManagerOperations
    {
        MMEntityCollection<IMMLayer> Layers { get; }
    }
}