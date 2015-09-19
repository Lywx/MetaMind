namespace MetaMind.Engine.Gui.Control.Item.Interactions
{
    using Service;

    public interface IViewItemViewSwapProvider
    {
        void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem);

        void ViewUpdateSwap();
    }
}