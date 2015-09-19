// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtGameTime.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.Xna.Framework
{
    using System;

    public static class GameTimeExt
    {
        public static TimeSpan Times(this GameTime gameTime, float time)
        {
            return TimeSpan.FromTicks((int)(time * gameTime.ElapsedGameTime.Ticks));
        }
    }
}