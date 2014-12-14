namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class TraceItemViewControl : ViewItemViewControl2D
    {
        public TraceItemViewControl(IViewItem item)
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

            ProcessManager.AttachProcess(new TraceItemSwapProcess(draggingItem, this.Item));
        }
    }
}