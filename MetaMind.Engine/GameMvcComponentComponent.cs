namespace MetaMind.Engine
{
    using System;

    public class GameMvcComponentComponent<TModule, TMvcSettings, TMvcLogic, TMvcVisual> : GameObject, IGameComponentModuleComponent<TMvcSettings>
        where                              TModule                                       : IGameMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                              TMvcLogic                                     : IGameMvcComponentLogic<TMvcSettings> 
        where                              TMvcVisual                                    : IGameMvcComponentVisual<TMvcSettings>
    {
        protected GameMvcComponentComponent(TModule module)
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

        public TMvcSettings Settings => this.Module.Settings;

        protected TModule Module { get; }

        protected TMvcLogic Logic => this.Module.Logic;

        protected TMvcVisual Visual => this.Module.Visual;

        #endregion

        #region Initialization

        public virtual void Initialize()
        {
        }

        #endregion
    }
}