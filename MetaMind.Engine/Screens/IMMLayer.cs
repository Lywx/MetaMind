namespace MetaMind.Engine.Screen
{
    using Nodes;

    public interface IMMLayer : IMMNode, IMMLayerOperations 
    {
        IMMScreen Screen { get; }
    }
}