namespace MetaMind.Engine.Entities.Controls.Item.Factories
{
    using Layers;

    public interface IViewItemFactory
    {
        IMMViewItemController CreateController(IMMViewItem item);

        IMMViewItemRendererComponent CreateRenderer(IMMViewItem item);

        IMMViewItemLayer CreateLayer(IMMViewItem item);
    }
}