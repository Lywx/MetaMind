namespace MetaMind.Runtime.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Runtime.Concepts.Tasks;

    public class TaskItemSwapProcessInMotivation : ViewItemSwapProcess
    {
        public TaskItemSwapProcessInMotivation(IViewItem draggingItem, IViewItem swappingItem, List<Task> commonSource)
            : base(draggingItem, swappingItem, commonSource)
        {
        }

        protected override void SwapInView()
        {
            this.SwapDataInList();

            base.SwapInView();
        }
    }
}