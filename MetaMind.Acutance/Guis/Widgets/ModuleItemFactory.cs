namespace MetaMind.Acutance.Guis.Widgets
{
    using MetaMind.Acutance.Concepts;
    using MetaMind.Acutance.Parsers.Elements;
    using MetaMind.Engine.Guis.Widgets.Items;

    public class ModuleItemFactory : ViewItemFactory2D
    {
        public ModuleItemFactory(IModulelist modulelist)
        {
            this.Modulelist = modulelist;
        }

        public IModulelist Modulelist { get; set; }

        public override dynamic CreateLogicControl(IViewItem item)
        {
            return new ModuleItemLogic(item, this.Modulelist.Modules);
        }

        public dynamic CreateData(RawKnowledgeFileBuffer buffer)
        {
            var module = this.Modulelist.Create(buffer.File);

            this.LinkData(buffer, module, new ModuleLinker(this, this.Modulelist));

            return module;
        }

        public override IItemVisual CreateVisualControl(IViewItem item)
        {
            return new ModuleItemVisual(item);
        }

        public void RemoveData(IViewItem item)
        {
            this.RemoveData(item.ItemData);
        }

        public void RemoveData(Module module)
        {
            this.Modulelist.Remove(module);

            this.RemoveSubData(module);

            module.Dispose();
        }

        private void LinkData(RawKnowledgeFileBuffer buffer, Module module, ModuleLinker linker)
        {
            linker.Initialize(module);

            foreach (var link in buffer.Links)
            {
                linker.Prepare(link);
            }

            linker.Start();
        }

        private void RemoveSubData(Module module)
        {
            if (module.Minions != null && 
                module.Minions.Count > 0)
            {
                foreach (var subModule in module.Minions.ToArray())
                {
                    this.RemoveData(subModule);
                }
            }
        }
    }
}