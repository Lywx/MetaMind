namespace MetaMind.Engine.Guis.Widgets.Items
{
    using MetaMind.Engine.Guis.Widgets.Views;

    public interface IViewItemExchangable : IViewItem
    {
        void ExchangeTo(IView towards, int position);
    }
}