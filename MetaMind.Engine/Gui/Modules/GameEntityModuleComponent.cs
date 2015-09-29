
namespace MetaMind.Engine.Gui.Modules
{
    public abstract class GameEntityModuleComponent<TModule, TModuleSettings, TModuleLogic> : GameInputableEntity, IInputable, IDrawable
        where                                       TModule                                 : GameEntityModule<TModuleSettings>
        where                                       TModuleLogic                            : GameEntityModuleLogic<TModule, TModuleSettings, TModuleLogic>
    {
        private readonly TModule module;

        protected GameEntityModuleComponent(TModule module)
        {
            this.module = module;
        }

        protected TModuleLogic Logic => (TModuleLogic)this.module.Logic;

        protected TModule Module => this.module;

        protected TModuleSettings Settings => this.module.Settings;
    }
}