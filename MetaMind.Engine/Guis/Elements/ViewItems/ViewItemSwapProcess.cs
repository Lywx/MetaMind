namespace MetaMind.Engine.Guis.Elements.ViewItems
{
    using System.Diagnostics;

    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.Views;

    using Microsoft.Xna.Framework;

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
            this.swapControl = swappingItem.ViewControl.Swap;
        }

        public override void OnAbort()
        {
        }

        public override void OnFail()
        {
        }

        public override void OnSuccess()
        {
            var inSameView = ReferenceEquals( this.swappingItem.View, this.draggedItem.View );
            if ( inSameView )
            {
                // swap id then sort
                var swappingId = this.swappingItem.ItemControl.Id;
                this.swappingItem.ItemControl.Id = this.draggedItem.ItemControl.Id;
                this.draggedItem.ItemControl.Id = swappingId;

                this.swappingItem.View.Control.SortItems( ViewSortMode.Id );
            }
            else
            {
                var draggedItemExchangable = this.draggedItem as IViewItemExchangable;
                var swappingItemExchangable = this.swappingItem as IViewItemExchangable;
                Debug.Assert( draggedItemExchangable != null && swappingItemExchangable != null );

                // replace each another in their origial view
                var swappingItemView = this.swappingItem.View;
                var draggedItemView = this.draggedItem.View;

                draggedItemExchangable.ExchangeTo( swappingItemView, this.swappingItem.ItemControl.Id );
                swappingItemExchangable.ExchangeTo( draggedItemView, this.draggedItem.ItemControl.Id );
            }

            // refine selection to make sure the overall effect is smooth
            this.draggedItem.ItemControl.SelectIt();

            // stop swapping state
            this.swappingItem.Disable( ItemState.Item_Swaping );
        }

        public override void OnUpdate( GameTime gameTime )
        {
            this.swapControl.Progress += 1f / updateNum;

            if ( this.swapControl.Progress > 1 )
                this.Succeed();
        }
    }
}