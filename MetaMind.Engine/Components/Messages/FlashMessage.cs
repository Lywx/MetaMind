// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlashMessage.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
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
            this.CurrentMessage = currentMessage;
            this.DisplayTime    = displayTime;
            this.CurrentIndex   = 0;
            this.Position       = position;
            this.DrawnMessage   = string.Empty;
            this.FontColor      = fontColor;
        }
    }
}