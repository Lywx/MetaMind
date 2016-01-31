namespace MetaMind.Engine.Core.Entity.Control.Item.Interactions
{
    using Services;

    public interface IViewItemViewSwapProvider
    {
        void ViewDoSwap(IMMEngineInteropService interop, IMMViewItem draggingItem);

        void ViewUpdateSwap();
    }
}