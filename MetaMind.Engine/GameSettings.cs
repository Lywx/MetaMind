namespace MetaMind.Engine
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Gui.Components;

    [DataContract]
    public class GameSettings : Component, IGameSettings
    {
        #region Dependency

        [DataMember]
        private GameSettingDictionary Dictionary { get; set; } =
            new GameSettingDictionary();

        #endregion

        #region Lookup

        public T Get<T>(string id)
        {
            return this.Dictionary.Get<T>(id);
        }

        #endregion

        #region Dictionary

        public void Add(KeyValuePair<string, object> item)
        {
            this.Dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.Dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            (this.Dictionary as ICollection<KeyValuePair<string, object>>).
                CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.Dictionary.Remove(item.Key);
        }

        public int Count => this.Dictionary.Count;

        public bool IsReadOnly => false;

        public bool ContainsKey(string key)
        {
            return this.Dictionary.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            this.Dictionary.Add(key, value);
        }

        public bool Remove(string key)
        {
            return this.Dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return this.Dictionary.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get { return this.Dictionary[key]; }
            set { this.Dictionary[key] = value; }
        }

        public ICollection<string> Keys => this.Dictionary.Keys;

        public ICollection<object> Values => this.Dictionary.Values;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.Dictionary).GetEnumerator();
        }

        #endregion
    }
}
