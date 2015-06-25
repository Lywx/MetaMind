namespace MetaMind.Engine.Guis.Layers
{
    using System.Collections.Generic;
    using Engine.Screens;

    public interface ILayerManager : ILayerManagerOperations
    {
        List<IGameLayer> GameLayers { get; }
    }
}