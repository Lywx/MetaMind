
namespace MetaMind.Engine.Entities
{
    public abstract class MMMVCEntityComponent<TMVCComponent, TMVCSettings, TMVCController> : MMInputEntity, IMMInputable, IMMDrawable
        where                                  TMVCComponent                                : MMMVCEntity<TMVCSettings>
        where                                  TMVCController                               : MMMVCEntityController<TMVCComponent, TMVCSettings, TMVCController>
    {
        private readonly TMVCComponent module;

        protected MMMVCEntityComponent(TMVCComponent module)
        {
            this.module = module;
        }

        protected TMVCController Controller => (TMVCController)this.module.Controller;

        protected TMVCComponent Module => this.module;

        protected TMVCSettings Settings => this.module.Settings;
    }
}