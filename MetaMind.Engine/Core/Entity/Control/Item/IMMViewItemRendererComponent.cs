namespace MetaMind.Engine.Core.Entity.Control.Item
{
    using Node.View;

    public interface IMMViewItemRendererComponentBase : IMMNodeRenderer
    {
        
    }

    public interface IMMViewItemRendererComponent : IMMViewItemRendererComponentBase, IMMViewItemComponentOperations
    {
        
    }
}