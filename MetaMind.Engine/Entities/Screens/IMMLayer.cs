namespace MetaMind.Engine.Entities.Screens
{
    using Bases;
    using Entities.Nodes;

    public interface IMMLayerOperations : IMMInputOperations, IMMUpdateableOperations, IMMInteropOperations 
    {
    }

    public interface IMMLayer : IMMNode, IMMLayerOperations 
    {
        IMMScreen Screen { get; }
    }
}