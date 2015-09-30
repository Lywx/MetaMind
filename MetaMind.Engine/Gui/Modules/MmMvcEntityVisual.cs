namespace MetaMind.Engine.Gui.Modules
{
    public abstract class MMMvcEntityVisual<TModule, TMvcSettings, TMvcLogic> : MMMvcEntityComponent<TModule, TMvcSettings, TMvcLogic>, IMMMvcEntityVisual
        where                          TModule                                 : MMMvcEntity<TMvcSettings>
        where                          TMvcLogic                            : MMMvcEntityLogic<TModule, TMvcSettings, TMvcLogic>
    {
        protected MMMvcEntityVisual(TModule module)
            : base(module)
        {
        }
    }
}