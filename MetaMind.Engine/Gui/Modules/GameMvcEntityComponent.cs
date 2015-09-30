
namespace MetaMind.Engine.Gui.Modules
{
    public abstract class GameMvcEntityComponent<TModule, TMvcSettings, TMvcLogic> : GameInputableEntity, IInputable, IDrawable
        where                                       TModule                                 : GameMvcEntity<TMvcSettings>
        where                                       TMvcLogic                            : GameMvcEntityLogic<TModule, TMvcSettings, TMvcLogic>
    {
        private readonly TModule module;

        protected GameMvcEntityComponent(TModule module)
        {
            this.module = module;
        }

        protected TMvcLogic Logic => (TMvcLogic)this.module.Logic;

        protected TModule Module => this.module;

        protected TMvcSettings Settings => this.module.Settings;
    }
}