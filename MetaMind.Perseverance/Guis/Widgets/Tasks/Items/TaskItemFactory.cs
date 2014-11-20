namespace MetaMind.Perseverance.Guis.Widgets.Tasks.Items
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    public class TaskItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new TaskItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return new TaskEntry();
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new TaskItemGraphics(item);
        }
    }
}