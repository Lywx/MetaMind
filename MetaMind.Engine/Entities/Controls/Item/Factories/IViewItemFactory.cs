namespace MetaMind.Engine.Entities.Controls.Item.Factories
{
    using Layers;

    public interface IViewItemFactory
    {
        IMMViewItemController CreateLogic(IMMViewItem item);

        IMMViewItemRendererComponent CreateVisual(IMMViewItem item);

        IMMViewItemLayer CreateLayer(IMMViewItem item);
    }
}