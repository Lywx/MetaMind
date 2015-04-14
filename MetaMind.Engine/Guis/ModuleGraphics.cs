namespace MetaMind.Engine.Guis
{
    public abstract class ModuleGraphics<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleGraphics
        where                            TModule                                   : Module         <TModuleSettings>
        where                            TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleGraphics(TModule module)
            : base(module)
        {
        }
    }
}