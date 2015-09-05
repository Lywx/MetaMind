namespace MetaMind.Engine.Guis.Controls.Items.Interactions
{
    using Services;

    public interface IViewItemViewSwapProvider
    {
        void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem);

        void ViewUpdateSwap();
    }
}