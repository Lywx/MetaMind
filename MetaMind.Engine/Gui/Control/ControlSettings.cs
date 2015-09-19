namespace MetaMind.Engine.Gui.Control
{
    using System;
    using System.Collections.Generic;
    using Service;

    [Serializable]
    public class ControlSettings : Dictionary<string, object>, IControlSettings
    {
        #region Dependency

        protected IGameInteropService Interop => GameEngine.Service.Interop;

        protected IGameNumericalService Numerical => GameEngine.Service.Numerical;

        protected IGameGraphicsService Graphics => GameEngine.Service.Graphics;

        protected IGameInputService Input => GameEngine.Service.Input;

        #endregion

        public T Get<T>(string id)
        {
            if (this.ContainsKey(id))
            {
                return (T)this[id];
            }

            throw new InvalidOperationException($"Settings contain no {id}");
        }
    }
}