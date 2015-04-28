namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class TraceItemFactory : ViewItemFactory2D
    {
        public ITracelist Tracelist { get; set; }

        public TraceItemFactory(ITracelist tracelist)
        {
            this.Tracelist = tracelist;
        }

        public override dynamic CreateLogicControl(IViewItem item)
        {
            return new TraceItemLogicControl(item, Tracelist.Traces);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return Tracelist.Create();
        }

        public override IItemVisualControl CreateVisualControl(IViewItem item)
        {
            return new TraceItemVisualControl(item);
        }

        public void RemoveData(IViewItem item)
        {
            Tracelist.Remove(item.ItemData);

            item.ItemData.Dispose();
        }
    }
}