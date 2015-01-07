namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class TraceItemFactory : ViewItemBasicFactory2D
    {
        public ITracelist Tracelist { get; set; }

        public TraceItemFactory(ITracelist tracelist)
        {
            this.Tracelist = tracelist;
        }

        public override dynamic CreateControl(IViewItem item)
        {
            return new TraceItemControl(item, Tracelist.Traces);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return Tracelist.Create();
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new TraceItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Tracelist.Remove(item.ItemData);

            item.ItemData.Dispose();
        }
    }
}