namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Services;

    public class GameModuleLogic<TGroup, TGroupSettings, TGroupLogic> : GameModuleComponent<TGroup, TGroupSettings, TGroupLogic>, IGameModuleLogic
        where                    TGroup                               : GameModule<TGroupSettings>
        where                    TGroupLogic                          : GameModuleLogic<TGroup, TGroupSettings, TGroupLogic>
    {
        protected GameModuleLogic(TGroup module, GameEngine engine)
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