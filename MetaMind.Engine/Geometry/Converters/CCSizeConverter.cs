// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Geometry.Converters
{
    using System.Collections.Generic;
    using Geometry;

    internal class MMSizeConverter
    {
        public static MMSize MMSizeFromString(string content)
        {
            MMSize ret = new MMSize();

            do
            {
                var strs = new List<string>();
                if (!MMUtils.SplitWithForm(content, strs)) break;

                float width  = MMUtils.MMParseFloat(strs[0]);
                float height = MMUtils.MMParseFloat(strs[1]);

                ret = new MMSize(width, height);
            }
            while (false);

            return ret;
        }

    }
}

