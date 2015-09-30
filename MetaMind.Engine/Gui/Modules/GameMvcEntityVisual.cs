namespace MetaMind.Engine.Gui.Modules
{
    public abstract class GameMvcEntityVisual<TModule, TMvcSettings, TMvcLogic> : GameMvcEntityComponent<TModule, TMvcSettings, TMvcLogic>, IGameMvcEntityVisual
        where                          TModule                                 : GameMvcEntity<TMvcSettings>
        where                          TMvcLogic                            : GameMvcEntityLogic<TModule, TMvcSettings, TMvcLogic>
    {
        protected GameMvcEntityVisual(TModule module)
            : base(module)
        {
        }
    }
}