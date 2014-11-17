namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using System.Collections.Generic;

    using MetaMind.Engine.Guis.Elements.ViewItems;
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
            // change data position
            MotivationTaskTracer parent = this.DraggedItem.View.Parent;
            List<TaskEntry>      tasks  = parent.FastHostData["Tasks"];

            int draggingPosition = tasks.IndexOf(this.DraggedItem .ItemData);
            int swappingPosition = tasks.IndexOf(this.SwappingItem.ItemData);

            tasks[draggingPosition] = this.SwappingItem.ItemData;
            tasks[swappingPosition] = this.DraggedItem .ItemData;
            
            base.SwapInView();
        }
    }
}