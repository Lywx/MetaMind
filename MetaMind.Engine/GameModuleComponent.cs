namespace MetaMind.Engine
{
    using System;

    public class GameModuleComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : IGameModuleComponent<TModuleSettings>
        where                        TModule                                                : IGameModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                        TModuleLogic                                           : IGameModuleLogic<TModuleSettings> 
        where                        TModuleVisual                                          : IGameModuleVisual<TModuleSettings>
    {
        protected GameModuleComponent(TModule module, GameEngine engine)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.Module = module;
            this.Engine = engine;
        }

        protected GameEngine Engine { get; private set; }

        protected TModule Module { get; }

        public TModuleSettings Settings => this.Module.Settings;

        protected TModuleLogic Logic => this.Module.Logic;

        protected TModuleVisual Visual => this.Module.Visual;

        public virtual void Initialize()
        {
        }
    }
}