using System;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Extensions
{
    public static class GameTimeExtension
    {
        public static TimeSpan DeltaTimeSpan( this GameTime gameTime, float multiple )
        {
            return TimeSpan.FromTicks( ( int ) ( multiple * gameTime.ElapsedGameTime.Ticks ) );
        }
    }
}