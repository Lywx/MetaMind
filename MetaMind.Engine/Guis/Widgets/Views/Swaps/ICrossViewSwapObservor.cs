namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public interface ICrossViewSwapObservor
    {
        void WatchSwapFrom(IViewItem draggingItem, IView targetView);
    }
}