using System;
using MetaMind.Engine.Guis.Widgets.Views;
using MetaMind.Engine.Guis.Widgets.Items;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemExchangable : ViewItemExchangeless, IViewItemExchangable
    {
        public ViewItemExchangable( IView view, ICloneable viewSettings, ICloneable itemSettings, IViewItemFactory itemFactory )
            : base( view, viewSettings, itemSettings, itemFactory )
        {
        }

        public void ExchangeTo( IView towards, int index )
        {
            View.Items.Remove( this );

            View = towards;
            View.Items.Insert( index, this );

            ViewSettings = towards.ViewSettings;
            ItemSettings = towards.ItemSettings;
        }
    }

    public interface IViewItemExchangable : IViewItem
    {
        void ExchangeTo( IView towards, int index );
    }
}