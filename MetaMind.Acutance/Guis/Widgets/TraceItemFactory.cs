namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;
    using MetaMind.Perseverance.Concepts.TaskEntries;

    public class TraceItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new TraceItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return new TaskEntry();
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new TraceItemGraphics(item);
        }
    }
}