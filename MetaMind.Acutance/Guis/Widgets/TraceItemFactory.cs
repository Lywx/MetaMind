namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;
    using MetaMind.Engine.Guis.Widgets.ViewItems;

    public class TraceItemFactory : ViewItemBasicFactory2D
    {
        public bool Positive;

        public TraceItemFactory(bool positive)
        {
            this.Positive = positive;
        }

        public override dynamic CreateControl(IViewItem item)
        {
            return new TraceItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            var trace = Acutance.Adventure.Tracelist.Create();
            trace.Positive = this.Positive;
            return trace;
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new TraceItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Acutance.Adventure.Tracelist.Remove(item.ItemData);
        }
    }
}