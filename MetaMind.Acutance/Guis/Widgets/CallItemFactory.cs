namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CallItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new CallItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            // here only provide dummy data
            return Acutance.Adventure.Calllist.Create(string.Empty, string.Empty, 0);
        }

        public CallEntry CreateData(string name, string path, int minutes)
        {
            return Acutance.Adventure.Calllist.Create(name, path, minutes);
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new CallItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Acutance.Adventure.Calllist.Remove(item.ItemData);
        }
    }
}