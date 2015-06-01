namespace MetaMind.Engine.Guis
{
    public abstract class ModuleLogic<TModule, TModuleSettings, TModuleLogic> : ModuleComponent<TModule, TModuleSettings, TModuleLogic>, IModuleLogicControl
        where                         TModule                                 : Module<TModuleSettings>
        where                         TModuleLogic                            : ModuleLogic<TModule, TModuleSettings, TModuleLogic>
    {
        protected ModuleLogic(TModule module)
            : base(module)
        {
        }
    }
}