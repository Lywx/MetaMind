namespace MetaMind.Engine.Entities.Controls.Item.Interactions
{
    using Services;

    public interface IViewItemViewSwapProvider
    {
        void ViewDoSwap(IMMEngineInteropService interop, IMMViewItem draggingItem);

        void ViewUpdateSwap();
    }
}