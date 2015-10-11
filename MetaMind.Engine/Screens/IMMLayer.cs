namespace MetaMind.Engine.Screens
{
    using Entities;
    using Entities.Nodes;

    public interface IMMLayerOperations : IMMInputOperations, IMMUpdateableOperations, IMMInteropOperations 
    {
    }

    public interface IMMLayer : IMMNode, IMMLayerOperations 
    {
        IMMScreen Screen { get; }
    }
}