namespace MetaMind.Engine
{
    using System;

    public class GameComponentComponent<TModule, TModuleSettings, TModuleLogic, TModuleVisual> : GameObject, IGameComponentModuleComponent<TModuleSettings>
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

            this.Module = module;
        }

        #region Engine Data

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