namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class TraceItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new TraceItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return Acutance.Session.Tracelist.Create();
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new TraceItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Acutance.Session.Tracelist.Remove(item.ItemData);
        }
    }
}