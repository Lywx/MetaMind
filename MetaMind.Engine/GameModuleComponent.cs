namespace MetaMind.Engine
{
    using System;

    public class GameModuleComponent<TGroup, TGroupSettings, TGroupLogic> : IGameModuleComponent<TGroupSettings>
        where                        TGroup                               : GameModule<TGroupSettings>
        where                        TGroupLogic                          : GameModuleLogic<TGroup, TGroupSettings, TGroupLogic>
    {
        private readonly TGroup module;

        protected GameModuleComponent(TGroup module, GameEngine engine)
        {
            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }

            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            this.module = module;
            this.Engine = engine;
        }

        protected GameEngine Engine { get; private set; }

        public TGroupSettings Settings => this.module.Settings;

        protected TGroupLogic Logic => (TGroupLogic)this.module.Logic;

        protected TGroup Module => this.module;

        public virtual void Initialize()
        {
        }
    }
}