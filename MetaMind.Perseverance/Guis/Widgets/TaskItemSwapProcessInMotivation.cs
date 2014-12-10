namespace MetaMind.Perseverance.Guis.Widgets
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Widgets.ViewItems;
    using MetaMind.Perseverance.Concepts.TaskEntries;
    using MetaMind.Perseverance.Guis.Modules;

    public class TaskItemSwapProcessInMotivation : ViewItemSwapProcess
    {
        public TaskItemSwapProcessInMotivation(IViewItem draggedItem, IViewItem swappingItem)
            : base(draggedItem, swappingItem)
        {
        }

        protected override void SwapInView()
        {
            // change data position in motivation's tasks
            MotivationTaskTracer parent = this.DraggedItem.View.Parent;
            List<TaskEntry>      tasks  = parent.FastHostData["Tasks"];

            this.SwapDataInList(tasks);

            base.SwapInView();
        }
    }
}