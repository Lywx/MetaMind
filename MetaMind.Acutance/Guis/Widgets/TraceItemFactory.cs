namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Guis.Elements.ViewItems;

    public class TraceItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new TraceItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            return Acutance.Adventure.Tracelist.Create();
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