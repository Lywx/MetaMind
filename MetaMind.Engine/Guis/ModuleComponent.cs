
namespace MetaMind.Engine.Guis
{
    public abstract class ModuleComponent<TModule, TModuleSettings, TModuleControl> : GameControllableEntity, IInputable, IDrawable
        where                             TModule                                   : Module<TModuleSettings>
        where                             TModuleControl                            : ModuleControl<TModule, TModuleSettings, TModuleControl>
    {
        private readonly TModule module;

        protected ModuleComponent(TModule module)
        {
            this.module = module;
        }

        protected TModuleControl Control
        {
            get
            {
                return (TModuleControl)this.module.Control;
            }
        }

        protected TModule Module
        {
            get
            {
                return this.module;
            }
        }

        public TModuleSettings ModuleSettings
        {
            get
            {
                return this.module.Settings;
            }
        }
    }
}