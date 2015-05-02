namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    using MetaMind.Engine.Services;

    public interface IViewItemViewSwapProvider
    {
        void ViewSwap(IGameInteropService interop, IViewItem draggingItem);
    }
}