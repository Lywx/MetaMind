namespace MetaMind.Engine.Entities
{
    public abstract class MMMVCEntityRenderer<TMVCComponent, TMVCSettings, TMVCController> : MMMVCEntityComponent<TMVCComponent, TMVCSettings, TMVCController>, IMMMVCEntityRenderer
        where                                 TMVCComponent                                : MMMVCEntity<TMVCSettings>
        where                                 TMVCController                               : MMMVCEntityController<TMVCComponent, TMVCSettings, TMVCController>
    {
        protected MMMVCEntityRenderer(TMVCComponent module)
            : base(module)
        {
        }
    }
}