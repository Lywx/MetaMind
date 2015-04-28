namespace MetaMind.Engine.Guis
{
    public abstract class ModuleLogicControl<TModule, TModuleSettings, TModuleLogic> : ModuleComponent<TModule, TModuleSettings, TModuleLogic>, IModuleLogicControl
        where                                TModule                                 : Module         <TModuleSettings>
        where                                TModuleLogic                            : ModuleLogicControl<TModule, TModuleSettings, TModuleLogic>
    {
        protected ModuleLogicControl(TModule module)
            : base(module)
        {
        }
    }
}