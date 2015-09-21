namespace MetaMind.Engine.Component.Content.Asset
{
    using System;
    using System.Collections.Generic;

    public class AssetList<T> : List<T>
    {
        #region Indexers 

        public T this[string index]
        {
            get
            {
                for (var i = 0; i < this.Count; i++)
                {
                    var s = (Asset)(object)this[i];
                    if (string.Equals(s.Name, index, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return this[i];
                    }
                }

                return default(T);
            }

            set
            {
                for (var i = 0; i < this.Count; i++)
                {
                    var s = (Asset)(object)this[i];
                    if (string.Equals(s.Name, index, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this[i] = value;
                    }
                }
            }
        }

        #endregion

        #region Constructors 

        public AssetList()
        {
        }


        public AssetList(AssetList<T> source)
        {
            for (var i = 0; i < source.Count; i++)
            {
                var t = new Type[1];
                t[0] = typeof(T);

                var p = new object[1];
                p[0] = source[i];

                // TODO: Unknown
                this.Add((T)t[0].GetConstructor(t).Invoke(p));
            }
        }

        #endregion
    }
}
