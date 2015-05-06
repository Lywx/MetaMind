namespace MetaMind.Engine.Guis.Widgets.Views.Swaps
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views.Logic;

    public interface ICrossViewSwapObservor
    {
        void WatchSwapFrom(IViewItem draggedItem, IView touchedView, IViewLogic touchedViewLogic);
    }
}