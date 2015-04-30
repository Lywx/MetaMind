namespace MetaMind.Engine.Guis.Widgets.Views.PointView.ViewSwap
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public interface ICrossViewTransitObservor
    {
        void WatchTrasitIn(IViewItem draggingItem, IView targetView);
    }
}