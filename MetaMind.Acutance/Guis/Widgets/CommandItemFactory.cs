namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CommandItemFactory : ViewItemFactory2D
    {
        public override dynamic CreateLogicControl(IViewItem item)
        {
            return new CommandItemLogicControl(item, Acutance.Session.Commandlist.Commands);
        }

        public override IItemVisualControl CreateVisualControl(IViewItem item)
        {
            return new CommandItemVisualControl(item);
        }

        public void RemoveData(IViewItem item)
        {
            Acutance.Session.Commandlist.Remove(item.ItemData);
        }
    }
}