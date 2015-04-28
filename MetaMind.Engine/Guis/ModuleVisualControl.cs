namespace MetaMind.Engine.Guis
{
    public abstract class ModuleVisualControl<TModule, TModuleSettings, TModuleLogic> : ModuleComponent<TModule, TModuleSettings, TModuleLogic>, IModuleVisualControl
        where                                 TModule                                 : Module         <TModuleSettings>
        where                                 TModuleLogic                            : ModuleLogicControl<TModule, TModuleSettings, TModuleLogic>
    {
        protected ModuleVisualControl(TModule module)
            : base(module)
        {
        }
    }
}