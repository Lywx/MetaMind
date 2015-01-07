namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class ModuleItemFactory : ViewItemBasicFactory2D
    {
        public ModuleItemFactory(IModulelist modulelist)
        {
            this.Modulelist = modulelist;
        }

        public IModulelist Modulelist { get; set; }

        public override dynamic CreateControl(IViewItem item)
        {
            return new ModuleItemControl(item, this.Modulelist.Modules);
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new TraceItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            this.Modulelist.Remove(item.ItemData);
        }
    }
}