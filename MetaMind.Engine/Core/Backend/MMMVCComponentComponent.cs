namespace MetaMind.Engine.Core.Backend
{
    using System;

    public class MMMVCComponentComponent<TMVCComponent, TMVCSettings, TMVCController, TMVCRenderer> : MMObject, IMMMvcComponentComponent<TMVCSettings>
        where                            TMVCComponent                                              : IMMMVCComponent<TMVCSettings, TMVCController, TMVCRenderer>
        where                            TMVCController                                             : IMMMVCComponentController<TMVCSettings> 
        where                            TMVCRenderer                                               : IMMMVCComponentRenderer<TMVCSettings>
    {
        protected MMMVCComponentComponent(TMVCComponent module)
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

        public TMVCSettings Settings => this.Module.Settings;

        protected TMVCComponent Module { get; }

        protected TMVCController Controller => this.Module.Controller;

        protected TMVCRenderer Renderer => this.Module.Renderer;

        #endregion

        #region Initialization

        public virtual void Initialize()
        {
        }

        #endregion
    }
}