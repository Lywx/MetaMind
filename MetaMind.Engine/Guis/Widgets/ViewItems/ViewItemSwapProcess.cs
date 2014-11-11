using MetaMind.Engine.Components.Processes;
using MetaMind.Engine.Guis.Widgets.Items;
using MetaMind.Engine.Guis.Widgets.Views;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MetaMind.Engine.Guis.Widgets.ViewItems
{
    public class ViewItemSwapProcess : ProcessBase
    {
        private const int updateNum = 6;

        private readonly IViewItem draggedItem;
        private readonly IViewItem swappingItem;
        private readonly IViewSwapControl swapControl;

        public ViewItemSwapProcess( IViewItem draggedItem, IViewItem swappingItem )
        {
            this.draggedItem = draggedItem;
            this.swappingItem = swappingItem;
            swapControl = swappingItem.ViewControl.Swap;
        }

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnSuccess()
        {
            var inSameView = ReferenceEquals( swappingItem.View, draggedItem.View );
            if ( inSameView )
            {
                // swap id then sort
                var swappingId = swappingItem.ItemControl.Id;
                swappingItem.ItemControl.Id = draggedItem.ItemControl.Id;
                draggedItem.ItemControl.Id = swappingId;

                swappingItem.View.Control.SortItems( ViewSortMode.Id );
            }
            else
            {
                var draggedItemExchangable = draggedItem as IViewItemExchangable;
                var swappingItemExchangable = swappingItem as IViewItemExchangable;
                Debug.Assert( draggedItemExchangable != null && swappingItemExchangable != null );

                // replace each another in their origial view
                var swappingItemView = swappingItem.View;
                var draggedItemView = draggedItem.View;

                draggedItemExchangable.ExchangeTo( swappingItemView, swappingItem.ItemControl.Id );
                swappingItemExchangable.ExchangeTo( draggedItemView, draggedItem.ItemControl.Id );
            }

            // refine selection to make sure the overall effect is smooth
            draggedItem.ItemControl.SelectIt();

            // stop swapping state
            swappingItem.Disable( ItemState.Item_Swaping );
        }

        public override void OnUpdate( GameTime gameTime )
        {
            swapControl.Progress += 1f / updateNum;

            if ( swapControl.Progress > 1 )
                Succeed();
        }
    }
}