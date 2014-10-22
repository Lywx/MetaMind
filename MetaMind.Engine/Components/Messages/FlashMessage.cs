using System;
using Microsoft.Xna.Framework;

namespace MetaMind.Engine.Components.Messages
{
    public struct FlashMessage
    {
        public int      CurrentIndex;
        public TimeSpan DisplayTime;
        public Color    FontColor;
        public string   DrawnMessage;
        public string   CurrentMessage;
        public Vector2  Position;

        public FlashMessage(string currentMessage, TimeSpan displayTime, Vector2 position, Color fontColor)
        {
            CurrentMessage = currentMessage;
            DisplayTime    = displayTime;
            CurrentIndex   = 0;
            Position       = position;
            DrawnMessage   = string.Empty;
            FontColor      = fontColor;
        }
    }
}
