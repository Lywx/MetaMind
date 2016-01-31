namespace MetaMind.Engine.Core.Entity.Screens
{
    using Entity.Common;
    using Entity.Node.Model;

    public interface IMMLayerOperations : IMMInputtableOperations, IMMUpdateableOperations, IMMLoadableOperations 
    {
    }

    public interface IMMLayer : IMMNode, IMMLayerOperations 
    {
        IMMScreen Screen { get; }
    }
}