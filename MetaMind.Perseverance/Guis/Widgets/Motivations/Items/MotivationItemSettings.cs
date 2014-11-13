// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemSettings.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Extensions;
    using MetaMind.Engine.Guis.Elements.Items;
    using MetaMind.Engine.Settings;

    using Microsoft.Xna.Framework;

    public class MotivationItemSettings : ItemSettings
    {
        public Color NameColor = Color.White;

        public Font  NameFont = Font.InfoSimSunFont;

        public Point NameFrameSize = new Point(256, 34);

        public int   NameLineMargin = 20;

        public float NameSize = 0.6f;

        public Color IdPendingColor = new Color(200, 200, 0, 2);

        public Color IdFramePendingColor = new Color(200, 200, 0, 2);

        public float SymbolFrameIncrementFactor = 0.2f;
        
        public Color FearColor = new Color(51, 204, 204, 223);

        public Color WishColor = new Color(255, 255, 255, 128);

        public MotivationItemSettings()
        {
        }
    }
}