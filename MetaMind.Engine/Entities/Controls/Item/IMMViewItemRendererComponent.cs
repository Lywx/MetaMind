namespace MetaMind.Engine.Entities.Controls.Item
{
    using Nodes;

    public interface IMMViewItemRendererComponentBase : IMMNodeRenderer
    {
        
    }

    public interface IMMViewItemRendererComponent : IMMViewItemRendererComponentBase, IMMViewItemComponentOperations
    {
        
    }
}