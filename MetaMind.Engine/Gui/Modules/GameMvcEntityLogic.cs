namespace MetaMind.Engine.Gui.Modules
{
    public abstract class GameMvcEntityLogic<TModule, TMvcSettings, TMvcLogic> : GameMvcEntityComponent<TModule, TMvcSettings, TMvcLogic>, IGameMvcEntityLogic
        where                                TModule                                 : GameMvcEntity<TMvcSettings>
        where                                TMvcLogic                            : GameMvcEntityLogic<TModule, TMvcSettings, TMvcLogic>
    {
        protected GameMvcEntityLogic(TModule module)
            : base(module)
        {
        }
    }
}