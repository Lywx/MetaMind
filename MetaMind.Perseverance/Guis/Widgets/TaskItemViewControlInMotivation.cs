namespace MetaMind.Perseverance.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Engine;
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts.Tasks;
    using MetaMind.Perseverance.Guis.Modules;

    public class TaskItemViewControlInMotivation : ViewItemViewControl2D
    {
        public TaskItemViewControlInMotivation(IViewItem item)
            : base(item)
        {
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

            List<Task> tasks;
            using (TaskModule parent = draggingItem.View.Parent)
            {
                tasks = parent.FastHostData["Tasks"];
            }

            ProcessManager.AttachProcess(new TaskItemSwapProcessInMotivation(draggingItem, Item, tasks));
        }
    }
}