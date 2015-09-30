// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtVector2.cs">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.Xna.Framework
{
    public static class Vector2Extension
    {
        public static Point ToPoint(this Vector2 vector2)
        {
            return new Point((int)vector2.X, (int)vector2.Y);
        }
    }
}