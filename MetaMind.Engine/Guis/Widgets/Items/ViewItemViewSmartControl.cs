namespace MetaMind.Engine.Guis.Widgets.Items
{
    public class ViewItemViewSmartControl<TViewItemWSwapProcess> : ViewItemViewControl2D
        where TViewItemWSwapProcess : ViewItemSwapProcess, new()
    {
        private dynamic dataSource;

        public ViewItemViewSmartControl(IViewItem item, dynamic dataSource)
            : base(item)
        {
            this.dataSource = dataSource;
        }

        public override void SwapIt(IViewItem draggingItem)
        {
            // state checking
            if (this.Item.IsEnabled(ItemState.Item_Swaping))
            {
                return;
            }

            // state control
            {
                this.Item.Enable(ItemState.Item_Swaping);
            }

            var originCenter = this        .ViewControl.Scroll.RootCenterPoint(this        .ItemControl.Id);
            var targetCenter = draggingItem.ViewControl.Scroll.RootCenterPoint(draggingItem.ItemControl.Id);

            ViewControl.Swap.Initialize(originCenter, targetCenter);

            var swapProcess = new TViewItemWSwapProcess().Initialize(draggingItem, this.Item, this.dataSource);
            ProcessManager.AttachProcess(swapProcess);
        }
    }
}