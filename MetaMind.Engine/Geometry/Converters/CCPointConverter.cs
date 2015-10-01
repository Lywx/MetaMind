// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Geometry.Converters
{
    using System.Collections.Generic;
    using Geometry;

    internal class MMPointConverter
    {
        public static MMPoint MMPointFromString(string content)
        {
            MMPoint ret = MMPoint.Zero;

            do
            {
                List<string> strs = new List<string>();
                if (!MMUtils.SplitWithForm(content, strs)) break;

                float x = MMUtils.MMParseFloat(strs[0]);
                float y = MMUtils.MMParseFloat(strs[1]);

                ret.X = x;
                ret.Y = y;

            } while (false);

            return ret;
        }

    }
}

