namespace MetaMind.Engine.Gui.Control.Skin
{
    using System;
    using System.Collections.Generic;

    public class SkinList<T> : List<T>
    {
        #region Indexers 

        public T this[string index]
        {
            get
            {
                for (var i = 0; i < this.Count; i++)
                {
                    var s = (SkinElement)(object)this[i];
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
                    var s = (SkinElement)(object)this[i];
                    if (string.Equals(s.Name, index, StringComparison.CurrentCultureIgnoreCase))
                    {
                        this[i] = value;
                    }
                }
            }
        }

        #endregion

        #region Constructors 

        public SkinList()
        {
        }


        public SkinList(SkinList<T> source)
        {
            for (var i = 0; i < source.Count; i++)
            {
                var t = new Type[1];
                t[0] = typeof(T);

                var p = new object[1];
                p[0] = source[i];

                this.Add((T)t[0].GetConstructor(t).Invoke(p));
            }
        }

        #endregion
    }
}
