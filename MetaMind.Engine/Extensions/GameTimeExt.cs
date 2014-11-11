using Microsoft.Xna.Framework;
using System;

namespace MetaMind.Engine.Extensions
{
    public static class GameTimeExt
    {
        public static TimeSpan DeltaTimeSpan( this GameTime gameTime, float multiple )
        {
            return TimeSpan.FromTicks( ( int ) ( multiple * gameTime.ElapsedGameTime.Ticks ) );
        }
    }
}