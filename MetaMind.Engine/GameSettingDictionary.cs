// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameSettingDictionary.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class GameSettingDictionary : Dictionary<string, object>
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