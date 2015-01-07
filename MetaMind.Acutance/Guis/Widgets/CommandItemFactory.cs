namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CommandItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new CommandItemControl(item, Acutance.Session.Commandlist.Commands);
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new CommandItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            Acutance.Session.Commandlist.Remove(item.ItemData);
        }
    }
}