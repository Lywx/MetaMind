namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GameModuleLogic<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : GameModuleComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual>, IGameModuleLogic<TModuleSettings>
        where                    TModule                                 : IGameModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                    TModuleLogic                            : IGameModuleLogic<TModuleSettings> 
        where                    TModuleVisual                           : IGameModuleVisual<TModuleSettings>
    {
        protected GameModuleLogic(TModule module, GameEngine engine)
            : base(module, engine)
        {
        }

        public virtual void Update(GameTime time)
        {
        }

        public virtual void UpdateInput(IGameInputService input, GameTime time)
        {
        }
    }
}