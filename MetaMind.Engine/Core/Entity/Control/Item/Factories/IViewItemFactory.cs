namespace MetaMind.Engine.Core.Entity.Control.Item.Factories
{
    using Layers;

    public interface IViewItemFactory
    {
        IMMViewItemController CreateController(IMMViewItem item);

        IMMViewItemRendererComponent CreateRenderer(IMMViewItem item);

        IMMViewItemLayer CreateLayer(IMMViewItem item);
    }
}