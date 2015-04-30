namespace MetaMind.Engine.Guis.Widgets.Items
{
    using System;

    using MetaMind.Engine.Guis.Widgets.Views;

    public class ViewItemExchangable : ViewItemExchangeless, IViewItemExchangable
    {
        public ViewItemExchangable(IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory)
            : base(view, viewSettings, itemSettings, itemFactory)
        {
        }

        public ViewItemExchangable(IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory, dynamic itemData)
            : base(view, viewSettings, itemSettings, itemFactory, (object)itemData)
        {
        }

        public void ExchangeTo(IView towards, int position)
        {
            // avoid possible self exchanging which is problematic
            // when there is a tempoarary id mismatach
            if (object.ReferenceEquals(this.View, towards))
            {
                return;
            }

            this.View.Items.Remove(this);

            this.View = towards;
            this.View.Items.Insert(position, this);

            this.ViewSettings = towards.ViewSettings;
            this.ItemSettings = towards.ItemSettings;
        }
    }
}