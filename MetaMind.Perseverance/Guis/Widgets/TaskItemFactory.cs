namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;
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