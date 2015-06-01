
namespace MetaMind.Engine.Guis
{
    public abstract class ModuleComponent<TModule, TModuleSettings, TModuleLogic> : GameControllableEntity, IInputable, IDrawable
        where                             TModule                                 : Module<TModuleSettings>
        where                             TModuleLogic                            : ModuleLogic<TModule, TModuleSettings, TModuleLogic>
    {
        private readonly TModule module;

        protected ModuleComponent(TModule module)
        {
            this.module = module;
        }

        protected TModuleLogic Logic
        {
            get
            {
                return (TModuleLogic)this.module.Logic;
            }
        }

        protected TModule Module
        {
            get
            {
                return this.module;
            }
        }

        protected TModuleSettings Settings
        {
            get
            {
                return this.module.Settings;
            }
        }
    }
}