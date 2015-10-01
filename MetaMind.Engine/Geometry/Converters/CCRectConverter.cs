// --------------------------------------------------------------------------------------------------------------------
// <copyright file="">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>

namespace MetaMind.Engine.Geometry.Converters
{
    using System.Collections.Generic;

    internal class MMRectConverter
    {
        public static MMRect MMRectFromString(string rectSpec)
        {
            MMRect result = MMRect.Zero;

            do
            {
                if (rectSpec == null)
                {
                    break;
                }

                string content = rectSpec;

                // find the first '{' and the third '}'
                int nPosLeft = content.IndexOf('{');
                int nPosRight = content.IndexOf('}');
                for (int i = 1; i < 3; ++i)
                {
                    if (nPosRight == -1)
                    {
                        break;
                    }
                    nPosRight = content.IndexOf('}', nPosRight + 1);
                }
                if (nPosLeft == -1 || nPosRight == -1)
                {
                    break;
                }
                content = content.Substring(nPosLeft + 1, nPosRight - nPosLeft - 1);
                int nPointEnd = content.IndexOf('}');
                if (nPointEnd == -1)
                {
                    break;
                }
                nPointEnd = content.IndexOf(',', nPointEnd);
                if (nPointEnd == -1)
                {
                    break;
                }

                // get the point string and size string
                string pointStr = content.Substring(0, nPointEnd);
                string sizeStr = content.Substring(nPointEnd + 1);
                //, content.Length - nPointEnd
                // split the string with ','
                List<string> pointInfo = new List<string>();

                if (!MMUtils.SplitWithForm(pointStr, pointInfo))
                {
                    break;
                }
                List<string> sizeInfo = new List<string>();
                if (!MMUtils.SplitWithForm(sizeStr, sizeInfo))
                {
                    break;
                }

                float x = MMUtils.MMParseFloat(pointInfo[0]);
                float y = MMUtils.MMParseFloat(pointInfo[1]);
                float width = MMUtils.MMParseFloat(sizeInfo[0]);
                float height = MMUtils.MMParseFloat(sizeInfo[1]);

                result = new MMRect(x, y, width, height);
            } while (false);

            return result;
        }
    }
}

