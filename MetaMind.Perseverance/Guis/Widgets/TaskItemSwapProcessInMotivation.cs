namespace MetaMind.Perseverance.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    public class TaskItemSwapProcessInMotivation : ViewItemSwapProcess
    {
        public TaskItemSwapProcessInMotivation(IViewItem draggingItem, IViewItem swappingItem, List<TaskEntry> source)
            : base(draggingItem, swappingItem, source)
        {
        }

        protected override void SwapInView()
        {
            this.SwapDataInList();

            base.SwapInView();
        }
    }
}