using MetaMind.Engine.Guis.Widgets.Views;
using System;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public interface IViewItemExchangable : IViewItem
    {
        void ExchangeTo( IView towards, int position );
    }

    public class ViewItemExchangable : ViewItemExchangeless, IViewItemExchangable
    {
        public ViewItemExchangable( IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory )
            : base( view, viewSettings, itemSettings, itemFactory )
        {
        }
        public ViewItemExchangable( IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory, object itemData)
            : base( view, viewSettings, itemSettings, itemFactory, itemData)
        {
        }

        public void ExchangeTo( IView towards, int position )
        {
            View.Items.Remove( this );

            View = towards;
            View.Items.Insert( position, this );

            ViewSettings = towards.ViewSettings;
            ItemSettings = towards.ItemSettings;
        }
    }
}