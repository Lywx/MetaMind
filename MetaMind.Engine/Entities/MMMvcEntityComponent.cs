
namespace MetaMind.Engine.Entities
{
    public abstract class MMMvcEntityComponent<TMvcComponent, TMvcSettings, TMvcLogic> : MMInputEntity, IMMInputable, IMMDrawable
        where                                  TMvcComponent                           : MMMvcEntity<TMvcSettings>
        where                                  TMvcLogic                               : MMMvcEntityLogic<TMvcComponent, TMvcSettings, TMvcLogic>
    {
        private readonly TMvcComponent module;

        protected MMMvcEntityComponent(TMvcComponent module)
        {
            this.module = module;
        }

        protected TMvcLogic Logic => (TMvcLogic)this.module.Logic;

        protected TMvcComponent Module => this.module;

        protected TMvcSettings Settings => this.module.Settings;
    }
}