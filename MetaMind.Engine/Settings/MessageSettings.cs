// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Settings
{
    using System;

    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    public static class MessageSettings
    {
        public static Font     MessageFont = Font.UiRegularFont;

        public static float    MessageSize = 1f;

        public static Color    MessageColor = Color.White;

        public static Vector2  MessagePosition = new Vector2(5, 5);

        public static TimeSpan MessageLastingPeriod = TimeSpan.FromSeconds(1.5);
    }
}