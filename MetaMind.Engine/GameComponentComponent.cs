namespace MetaMind.Engine
{
    using System;
    using Service;

    public class GameComponentComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : IGameComponentModuleComponent<TModuleSettings>
        where                           TModule                                                : IGameComponentModule<TModuleSettings, TModuleLogic, TModuleVisual>
        where                           TModuleLogic                                           : IGameComponentLogic<TModuleSettings> 
        where                           TModuleVisual                                          : IGameComponentVisual<TModuleSettings>
    {
        protected GameComponentComponent(TModule module, GameEngine engine)
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

        #region Engine Data

        protected GameEngine Engine { get; private set; }

        protected IGameInputService Input => GameEngine.Service.Input;

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        #endregion

        #region Module Data

        public TModuleSettings Settings => this.Module.Settings;

        protected TModule Module { get; }

        protected TModuleLogic Logic => this.Module.Logic;

        protected TModuleVisual Visual => this.Module.Visual;

        #endregion

        #region Initialization

        public virtual void Initialize()
        {
        }

        #endregion
    }
}