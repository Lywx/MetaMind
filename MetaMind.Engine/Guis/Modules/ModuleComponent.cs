using MetaMind.Engine.Guis.Widgets;

namespace MetaMind.Engine.Guis.Modules
{
    public abstract class ModuleComponent<TModule, TModuleSettings, TModuleControl> : EngineObject 
        where                             TModule                                   : Module<TModuleSettings>
        where                             TModuleControl                            : ModuleControl<TModule, TModuleSettings, TModuleControl>
    {
        private readonly TModule module;

        protected ModuleComponent( TModule module )
        {
            this.module = module;
        }

        protected TModuleControl  Control        { get { return ( TModuleControl ) module.Control; } }
        protected TModule         Module         { get { return module; } }
        public    TModuleSettings ModuleSettings { get { return module.Settings; } }
    }
}