namespace MetaMind.Engine.Guis.Widgets.Items.Interactions
{
    using MetaMind.Engine.Services;

    public interface IViewItemViewSwapProvider
    {
        void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem);

        void ViewUpdateSwap();
    }
}