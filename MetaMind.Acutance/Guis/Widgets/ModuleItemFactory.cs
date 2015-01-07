namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class ModuleItemFactory : ViewItemBasicFactory2D
    {
        public ModuleItemFactory(IModulelist modulelist)
        {
            this.Modulelist = modulelist;
        }

        public IModulelist Modulelist { get; set; }

        public dynamic CreateData(KnowledgeFile file)
        {
            return this.Modulelist.Create(file);
        }

        public override dynamic CreateControl(IViewItem item)
        {
            return new ModuleItemControl(item, this.Modulelist.Modules);
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new ModuleItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            this.Modulelist.Remove(item.ItemData);

            item.ItemData.Dispose();
        }
    }
}