namespace MetaMind.Engine.Guis.Widgets.Items.ItemView
{
    using MetaMind.Engine.Services;

    public interface IViewSwapContent
    {
        void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem);
    }
}