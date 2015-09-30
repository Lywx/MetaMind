namespace MetaMind.Engine
{
    using System;

    public class MMMvcComponentComponent<TModule, TMvcSettings, TMvcLogic, TMvcVisual> : MMObject, IMMMvcComponentComponent<TMvcSettings>
        where                              TModule                                       : IMMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                              TMvcLogic                                     : IMMMvcComponentLogic<TMvcSettings> 
        where                              TMvcVisual                                    : IMMMvcComponentVisual<TMvcSettings>
    {
        protected MMMvcComponentComponent(TModule module)
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