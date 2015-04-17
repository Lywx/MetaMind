// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;

    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    public class MessageSettings
    {
        public Font     MessageFont = Font.UiRegular;

        public float    MessageSize = 1f;

        public Color    MessageColor = Color.White;

        public Vector2  MessagePosition = new Vector2(5, 5);

        public TimeSpan MessageLife = TimeSpan.FromSeconds(1.5);
    }
}