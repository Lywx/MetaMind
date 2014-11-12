// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlashMessage.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Messages
{
    using System;

    using Microsoft.Xna.Framework;

    public struct FlashMessage
    {
        public int CurrentIndex;

        public string CurrentMessage;

        public TimeSpan DisplayTime;

        public string DrawnMessage;

        public Color FontColor;

        public Vector2 Position;

        public FlashMessage(string currentMessage, TimeSpan displayTime, Vector2 position, Color fontColor)
        {
            CurrentMessage = currentMessage;
            DisplayTime = displayTime;
            CurrentIndex = 0;
            Position = position;
            DrawnMessage = string.Empty;
            FontColor = fontColor;
        }
    }
}