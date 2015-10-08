namespace MetaMind.Engine.Gui.Controls.Item.Interactions
{
    using Services;

    public interface IViewItemViewSwapProvider
    {
        void ViewDoSwap(IMMEngineInteropService interop, IViewItem draggingItem);

        void ViewUpdateSwap();
    }
}