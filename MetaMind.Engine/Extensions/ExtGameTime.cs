// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtGameTime.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Extensions
{
    using System;

    using Microsoft.Xna.Framework;

    public static class ExtGameTime
    {
        public static TimeSpan Times(this GameTime gameTime, float time)
        {
            return TimeSpan.FromTicks((int)(time * gameTime.ElapsedGameTime.Ticks));
        }
    }
}