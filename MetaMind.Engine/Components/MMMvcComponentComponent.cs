namespace MetaMind.Engine.Components
{
    using System;

    public class MMMvcComponentComponent<TMvcComponent, TMvcSettings, TMvcLogic, TMvcVisual> : MMObject, IMMMvcComponentComponent<TMvcSettings>
        where                            TMvcComponent                                       : IMMMvcComponent<TMvcSettings, TMvcLogic, TMvcVisual>
        where                            TMvcLogic                                     : IMMMvcComponentLogic<TMvcSettings> 
        where                            TMvcVisual                                    : IMMMvcComponentVisual<TMvcSettings>
    {
        protected MMMvcComponentComponent(TMvcComponent module)
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

        protected TMvcComponent Module { get; }

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