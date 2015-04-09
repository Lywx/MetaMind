namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Perseverance.Concepts.Tasks;

    public class TaskItemFactory : ViewItemFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new TaskItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return new Task();
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new TaskItemGraphics(item);
        }
    }
}