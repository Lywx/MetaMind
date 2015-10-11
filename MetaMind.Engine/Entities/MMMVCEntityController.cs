namespace MetaMind.Engine.Entities
{
    public abstract class MMMVCEntityController<TMVCComponent, TMVCSettings, TMVCController> : MMMVCEntityComponent<TMVCComponent, TMVCSettings, TMVCController>, IMMMVCEntityController 
        where                                   TMVCComponent                                : MMMVCEntity<TMVCSettings>
        where                                   TMVCController                               : MMMVCEntityController<TMVCComponent, TMVCSettings, TMVCController>
    {
        protected MMMVCEntityController(TMVCComponent module)
            : base(module)
        {
        }
    }
}