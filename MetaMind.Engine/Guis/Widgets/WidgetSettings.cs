namespace MetaMind.Engine.Guis.Widgets
{
    using System;
    using System.Collections.Generic;

    public class WidgetSettings : Dictionary<string, object>
    {
        public T Get<T>(string key)
        {
            if (this.ContainsKey(key))
            {
                return (T)this[key];
            }

            throw new InvalidOperationException(string.Format("Settings contain no {0}", key));
        }
    }
}