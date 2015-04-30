namespace MetaMind.Engine.Guis.Widgets.Items.ItemView
{
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    public interface IViewTransitContent
    {
        void ViewDoTransit(IGameInteropService interop, IViewItem draggingItem, IView targetView);
    }
}