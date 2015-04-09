// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtVector2.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Extensions
{
    using Microsoft.Xna.Framework;

    public static class ExtVector2
    {
        public static Point ToPoint(this Vector2 vector2)
        {
            return new Point((int)vector2.X, (int)vector2.Y);
        }
    }
}