namespace MetaMind.Engine.Guis.Widgets
{
    using System;
    using System.Collections.Generic;

    public class WidgetSettings : Dictionary<string, object>
    {
        public T Get<T>(string id)
        {
            if (this.ContainsKey(id))
            {
                return (T)this[id];
            }

            throw new InvalidOperationException(string.Format("Settings contain no {0}", id));
        }
    }
}