namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Engine.Guis.Elements.Views;

    public class MotivationItemViewControl : ViewItemViewControl1D
    {
        public MotivationItemViewControl(IViewItem item)
            : base(item)
        {
        }

        public override void ExchangeIt(IViewItem draggingItem, IView targetView)
        {
            if (Item.IsEnabled(ItemState.Item_Exchanging))
            {
                return;
            }

            Item.Enable(ItemState.Item_Exchanging);

            ProcessManager.AttachProcess(new MotivationItemExchangeProcess(draggingItem, targetView));
            
        }

        public override void SwapIt(IViewItem draggingItem)
        {
            if (this.Item.IsEnabled(ItemState.Item_Swaping))
            {
                return;
            }

            this.Item.Enable(ItemState.Item_Swaping);

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint(this        .ItemControl.Id);
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint(draggingItem.ItemControl.Id);

            this.ViewControl.Swap.Initialize(originCenter, targetCenter);

            ProcessManager.AttachProcess(new MotivationItemSwapProcess(draggingItem, this.Item));
        }
    }
}