// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanArrayExtension.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
namespace MetaMind.Engine.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class BooleanArrayExtension
    {
        public static List<string> ToString(bool[] states, Type state)
        {
            var stateList = new List<string>();

            for (var i = 0; i < states.Count(); ++i)
            {
                stateList.Add(states[i] ? Enum.GetName(state, i) : string.Empty);
            }

            return stateList;
        }
    }
}