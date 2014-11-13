namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using System;

    using MetaMind.Engine.Guis.Elements.Views;

    public interface IViewItemExchangable : IViewItem
    {
        void ExchangeTo(IView towards, int position);
    }

    public class ViewItemExchangable : ViewItemExchangeless, IViewItemExchangable
    {
        public ViewItemExchangable(IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        public ViewItemExchangable(IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory, object itemData)
            : base(view, viewSettings, itemSettings, itemFactory, itemData)
        {
        }

        public void ExchangeTo(IView towards, int position)
        {
            this.View.Items.Remove(this);

            this.View = towards;
            this.View.Items.Insert(position, this);

            this.ViewSettings = towards.ViewSettings;
            this.ItemSettings = towards.ItemSettings;
        }
    }
}