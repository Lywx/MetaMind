namespace MetaMind.Engine.Guis.Widgets.Items.Views
{
    using MetaMind.Engine.Guis.Widgets.Items.Swaps;
    using MetaMind.Engine.Services;

    public class ViewItemViewSmartControl<TViewItemWSwapProcess> : ViewItemView2DControl
        where TViewItemWSwapProcess : ViewItemSwapProcess, new()
    {
        private dynamic dataSource;

        public ViewItemViewSmartControl(IViewItem item, dynamic dataSource)
            : base(item)
        {
            this.dataSource = dataSource;
        }

        public override void ViewDoSwap(IGameInteropService interop, IViewItem draggingItem)
        {
            // state checking
            if (this.Item[ItemState.Item_Is_Swaping]())
            {
                return;
            }

            // state control
            {
                this.Item[ItemState.Item_Is_Swaping] = () => true;
            }

            var originCenter = this        .ViewLogic.Scroll.RootCenterPoint(this        .ItemLogic.Id);
            var targetCenter = draggingItem.ViewLogic.Scroll.RootCenterPoint(draggingItem.ItemLogic.Id);

            this.ViewLogic.Swap.Initialize(originCenter, targetCenter);

            var swapProcess = new TViewItemWSwapProcess().Initialize(draggingItem, this.Item, this.dataSource);
            interop.Process.AttachProcess(swapProcess);
        }
    }
}