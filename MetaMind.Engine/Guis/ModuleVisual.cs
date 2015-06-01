namespace MetaMind.Engine.Guis
{
    public abstract class ModuleVisual<TModule, TModuleSettings, TModuleLogic> : ModuleComponent<TModule, TModuleSettings, TModuleLogic>, IModuleVisualControl
        where                          TModule                                 : Module<TModuleSettings>
        where                          TModuleLogic                            : ModuleLogic<TModule, TModuleSettings, TModuleLogic>
    {
        protected ModuleVisual(TModule module)
            : base(module)
        {
        }
    }
}