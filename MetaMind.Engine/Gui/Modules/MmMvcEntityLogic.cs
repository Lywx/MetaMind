namespace MetaMind.Engine.Gui.Modules
{
    public abstract class MMMvcEntityLogic<TModule, TMvcSettings, TMvcLogic> : MMMvcEntityComponent<TModule, TMvcSettings, TMvcLogic>, IGameMvcEntityLogic
        where                                TModule                                 : MMMvcEntity<TMvcSettings>
        where                                TMvcLogic                            : MMMvcEntityLogic<TModule, TMvcSettings, TMvcLogic>
    {
        protected MMMvcEntityLogic(TModule module)
            : base(module)
        {
        }
    }
}