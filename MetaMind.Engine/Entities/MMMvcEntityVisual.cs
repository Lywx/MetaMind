namespace MetaMind.Engine.Entities
{
    public abstract class MMMvcEntityVisual<TMvcComponent, TMvcSettings, TMvcLogic> : MMMvcEntityComponent<TMvcComponent, TMvcSettings, TMvcLogic>, IMMMvcEntityVisual
        where                               TMvcComponent                           : MMMvcEntity<TMvcSettings>
        where                               TMvcLogic                               : MMMvcEntityLogic<TMvcComponent, TMvcSettings, TMvcLogic>
    {
        protected MMMvcEntityVisual(TMvcComponent module)
            : base(module)
        {
        }
    }
}