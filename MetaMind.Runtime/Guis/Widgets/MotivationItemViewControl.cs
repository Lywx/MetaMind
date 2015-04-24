namespace MetaMind.Runtime.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.Views;
    using MetaMind.Engine.Services;

    public class MotivationItemViewControl : ViewItemViewControl1D
    {
        public MotivationItemViewControl(IViewItem item)
            : base(item)
        {
        }

        public override void ExchangeIt(IGameInteropService interop, IViewItem draggingItem, IView targetView)
        {
            if (this.Item.IsEnabled(ItemState.Item_Exchanging))
            {
                return;
            }

            this.Item.Enable(ItemState.Item_Exchanging);

            interop.Process.AttachProcess(new MotivationItemTransitProcess(draggingItem, targetView));
        }

        public override void SwapIt(IGameInteropService interop, IViewItem draggingItem)
        {
            if (this.Item.IsEnabled(ItemState.Item_Swaping))
            {
                return;
            }

            this.Item.Enable(ItemState.Item_Swaping);

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint(this        .ItemControl.Id);
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint(draggingItem.ItemControl.Id);

            this.ViewControl.Swap.Initialize(originCenter, targetCenter);

            interop.Process.AttachProcess(new MotivationItemSwapProcess(draggingItem, this.Item));
        }
    }
}