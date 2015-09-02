namespace MetaMind.Engine
{
    public abstract class GameEntityModuleLogic<TModule, TModuleSettings, TModuleLogic> : GameEntityModuleComponent<TModule, TModuleSettings, TModuleLogic>, IGameEntityModuleLogic
        where                         TModule                                 : GameEntityModule<TModuleSettings>
        where                         TModuleLogic                            : GameEntityModuleLogic<TModule, TModuleSettings, TModuleLogic>
    {
        protected GameEntityModuleLogic(TModule module)
            : base(module)
        {
        }
    }
}