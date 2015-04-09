namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;

    public class MotivationItemViewControl : ViewItemViewControl1D
    {
        public MotivationItemViewControl(IViewItem item)
            : base(item)
        {
        }

        public override void ExchangeIt(IViewItem draggingItem, IView targetView)
        {
            if (this.Item.IsEnabled(ItemState.Item_Exchanging))
            {
                return;
            }

            this.Item.Enable(ItemState.Item_Exchanging);

            ProcessManager.AttachProcess(new MotivationItemTransitProcess(draggingItem, targetView));
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