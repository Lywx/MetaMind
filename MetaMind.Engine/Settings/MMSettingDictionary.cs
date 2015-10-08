namespace MetaMind.Engine.Settings
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class MMSettingDictionary : Dictionary<string, object>
    {
        #region Lookup

        public T Get<T>(string id)
        {
            if (this.ContainsKey(id))
            {
                return (T)this[id];
            }

            throw new InvalidOperationException($"Settings contain no {id}");
        }

        #endregion
    }
}
