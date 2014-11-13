namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using System.Diagnostics;

    using MetaMind.Engine.Components.Processes;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    public class MotivationItemViewControl : ViewItemViewControl1D
    {
        public MotivationItemViewControl(IViewItem item)
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

            ProcessManager.AttachProcess(new MotivationItemSwapProcess(draggingItem, this.Item));
        }
    }
}