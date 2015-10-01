namespace MetaMind.Engine.Entities
{
    public abstract class MMMvcEntityLogic<TMvcComponent, TMvcSettings, TMvcLogic> : MMMvcEntityComponent<TMvcComponent, TMvcSettings, TMvcLogic>, IMMMvcEntityLogic
        where                                TMvcComponent                                 : MMMvcEntity<TMvcSettings>
        where                                TMvcLogic                            : MMMvcEntityLogic<TMvcComponent, TMvcSettings, TMvcLogic>
    {
        protected MMMvcEntityLogic(TMvcComponent module)
            : base(module)
        {
        }
    }
}