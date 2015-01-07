namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Engine.Guis.Widgets.Items;

    public class CommandItemFactory : ViewItemBasicFactory2D
    {
        public override dynamic CreateControl(IViewItem item)
        {
            return new CommandItemControl(item);
        }

        // TODO: do I need to generate command entry inside command view?
        //public CommandEntry CreateData(string name, string path, int lineNum)
        //{
        //    return Acutance.Session.Commandlist.Create(name, path);
        //}

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