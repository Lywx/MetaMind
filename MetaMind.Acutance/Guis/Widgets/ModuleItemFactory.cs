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

        public override dynamic CreateControl(IViewItem item)
        {
            return new ModuleItemControl(item, this.Modulelist.Modules);
        }

        public dynamic CreateData(KnowledgeFileBuffer buffer)
        {
            var module = this.Modulelist.Create(buffer.File);

            this.LinkData(buffer, module, new ModuleLinker(this, this.Modulelist));

            return module;
        }

        public override IItemGraphics CreateGraphics(IViewItem item)
        {
            return new ModuleItemGraphics(item);
        }

        public void RemoveData(IViewItem item)
        {
            this.RemoveData(item.ItemData);
        }

        public void RemoveData(ModuleEntry module)
        {
            this.Modulelist.Remove(module);

            this.RemoveSubData(module);

            module.Dispose();
        }

        private void LinkData(KnowledgeFileBuffer buffer, ModuleEntry module, ModuleLinker linker)
        {
            linker.Initialize(module);

            foreach (var link in buffer.Links)
            {
                linker.Prepare(link);
            }

            linker.Start();
        }

        private void RemoveSubData(ModuleEntry module)
        {
            if (module.SubModuleEntries != null && 
                module.SubModuleEntries.Count > 0)
            {
                foreach (var subModule in module.SubModuleEntries.ToArray())
                {
                    this.RemoveData(subModule);
                }
            }
        }
    }
}