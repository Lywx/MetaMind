using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMind.Engine.Extensions
{
    using Microsoft.Xna.Framework;

    public static class PointUtils
    {
        public static Point Parse(string str)
        {
            var x = 0;
            var y = 0;

            var value = str.Split(',');

            if (value.Length >= 1)
            {
                x = int.Parse(value[0]);
            }

            if (value.Length >= 2)
            {
                y = int.Parse(value[1]);
            }

            return new Point(x, y);
        }
    }
}
