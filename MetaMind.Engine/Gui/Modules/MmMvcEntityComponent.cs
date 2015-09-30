
namespace MetaMind.Engine.Gui.Modules
{
    public abstract class MMMvcEntityComponent<TModule, TMvcSettings, TMvcLogic> : MMInputableEntity, IMMInputable, IMMDrawable
        where                                       TModule                                 : MMMvcEntity<TMvcSettings>
        where                                       TMvcLogic                            : MMMvcEntityLogic<TModule, TMvcSettings, TMvcLogic>
    {
        private readonly TModule module;

        protected MMMvcEntityComponent(TModule module)
        {
            this.module = module;
        }

        protected TMvcLogic Logic => (TMvcLogic)this.module.Logic;

        protected TModule Module => this.module;

        protected TMvcSettings Settings => this.module.Settings;
    }
}