namespace MetaMind.Engine.Gui.Controls.Item.Interactions
{
    using Service;

    public interface IViewItemViewSwapProvider
    {
        void ViewDoSwap(IMMEngineInteropService interop, IViewItem draggingItem);

        void ViewUpdateSwap();
    }
}