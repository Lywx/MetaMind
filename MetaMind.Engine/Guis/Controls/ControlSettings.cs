namespace MetaMind.Engine.Guis.Controls
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ControlSettings : Dictionary<string, object>, IControlSettings
    {
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