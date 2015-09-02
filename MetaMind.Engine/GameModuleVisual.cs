namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GameModuleVisual<TModule, TModuleSettings, TModuleLogic> : GameModuleComponent<TModule, TModuleSettings, TModuleLogic>, IGameModuleVisual
        where                     TModule                                 : GameModule<TModuleSettings>
        where                     TModuleLogic                            : GameModuleLogic<TModule, TModuleSettings, TModuleLogic>
    {
        public GameModuleVisual(TModule module, GameEngine engine)
            : base(module, engine)
        {
        }

        public virtual void Update(GameTime time)
        {
        }

        public virtual void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }
    }
}