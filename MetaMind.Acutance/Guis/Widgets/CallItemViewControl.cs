namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CallItemViewControl : ViewItemViewControl2D
    {
        public CallItemViewControl(IViewItem item)
            : base(item)
        {
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

            ProcessManager.AttachProcess(new CallItemSwapProcess(draggingItem, this.Item, Acutance.Adventure.Calllist.Calls));
        }
    }
}