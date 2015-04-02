namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class SmartItemViewControl<TViewItemWSwapProcess> : ViewItemViewControl2D
        where TViewItemWSwapProcess : ViewItemSwapProcess, new()
    {
        private dynamic source;

        public SmartItemViewControl(IViewItem item, dynamic source)
            : base(item)
        {
            this.source = source;
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

            ViewControl.Swap.Initialize(originCenter, targetCenter);

            var swapProcess = new TViewItemWSwapProcess().Initalize(draggingItem, this.Item, this.source);
            ProcessManager.AttachProcess(swapProcess);
        }
    }
}