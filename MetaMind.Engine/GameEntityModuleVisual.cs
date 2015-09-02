namespace MetaMind.Engine
{
    public abstract class GameEntityModuleVisual<TModule, TModuleSettings, TModuleLogic> : GameEntityModuleComponent<TModule, TModuleSettings, TModuleLogic>, IGameEntityModuleVisual
        where                          TModule                                 : GameEntityModule<TModuleSettings>
        where                          TModuleLogic                            : GameEntityModuleLogic<TModule, TModuleSettings, TModuleLogic>
    {
        protected GameEntityModuleVisual(TModule module)
            : base(module)
        {
        }
    }
}