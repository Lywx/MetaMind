namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GameModuleVisual<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : GameModuleComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual>, IGameModuleVisual<TModuleSettings>
        where                     TModule                                 : IGameModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                     TModuleLogic                            : IGameModuleLogic<TModuleSettings> 
        where                     TModuleVisual                           : IGameModuleVisual<TModuleSettings>
    {
        protected GameModuleVisual(TModule module, GameEngine engine)
            : base(module, engine)
        {
        }

        public virtual void Update(GameTime time)
        {
        }

        public virtual void BeginDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }

        public virtual void Draw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }

        public virtual void EndDraw(IGameGraphicsService graphics, GameTime time, byte alpha)
        {
        }
    }
}