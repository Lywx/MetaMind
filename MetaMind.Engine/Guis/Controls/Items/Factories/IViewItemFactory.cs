namespace MetaMind.Engine.Guis.Controls.Items.Factories
{
    using Layers;
    using Logic;
    using Visuals;

    public interface IViewItemFactory
    {
        IViewItemLogic CreateLogic(IViewItem item);

        IViewItemVisual CreateVisual(IViewItem item);

        IViewItemLayer CreateLayer(IViewItem item);
    }
}