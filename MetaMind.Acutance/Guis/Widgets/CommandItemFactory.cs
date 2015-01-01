namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CommandItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new CommandItemControl(item);
        }

        public override dynamic CreateData(IViewItem item)
        {
            // here only provide dummy data
            return Acutance.Adventure.Commandlist.Create(string.Empty, string.Empty, 0);
        }

        public CommandEntry CreateData(string name, string path, int minutes)
        {
            return Acutance.Adventure.Commandlist.Create(name, path, minutes);
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new CommandItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Acutance.Adventure.Commandlist.Remove(item.ItemData);
        }
    }
}