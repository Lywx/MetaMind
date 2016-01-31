
namespace MetaMind.Engine.Core.Entity
{
    using Entity.Common;

    public abstract class MMMVCEntityComponent<TMVCComponent, TMVCSettings, TMVCController> : MMInputtableEntity, IMMInputtable, IMMDrawable
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