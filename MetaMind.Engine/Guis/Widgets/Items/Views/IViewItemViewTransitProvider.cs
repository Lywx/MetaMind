namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    public interface IViewItemViewTransitProvider
    {
        void ViewTransit(IGameInteropService interop, IViewItem draggingItem, IView targetView);
    }
}