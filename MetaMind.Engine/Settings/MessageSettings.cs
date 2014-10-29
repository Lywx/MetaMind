using System;
using MetaMind.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MetaMind.Engine.Settings
{
    public static class MessageSettings
    {
        public static Font     MessageFont          = Font.UiRegularFont;
        public static float    MessageSize          = 1f;
        public static Color    MessageColor         = Color.White;
        public static Vector2  MessagePosition      = new Vector2( 5, 5 );
        public static TimeSpan MessageLastingPeriod = TimeSpan.FromSeconds( 1.5 );
    }
}