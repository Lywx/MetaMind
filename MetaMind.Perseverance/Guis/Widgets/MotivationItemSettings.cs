// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemSettings.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Perseverance.Guis.Widgets
{
    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Guis.Widgets.Items;

    using Microsoft.Xna.Framework;

    public class MotivationItemSettings : ItemSettings
    {
        public Color NameColor                  = Color.White;

        public Font  NameFont                   = Font.ContentRegular;

        public Point NameFrameSize              = new Point(256, 34);

        public int   NameLineMargin             = 20;

        public float NameSize                   = 0.6f;

        public Point NameMargin                 = new Point(0, 40);

        //---------------------------------------------------------------------
        public Color IdPendingColor             = new Color(200, 200, 0, 2);

        public Color IdFramePendingColor        = new Color(200, 200, 0, 2);

        //---------------------------------------------------------------------
        public float SymbolFrameIncrementFactor = 0.2f;
        
        public Color SymbolFrameFearColor       = new Color(51, 204, 204, 223);

        public Color SymbolFrameWishColor       = new Color(255, 255, 255, 128);

        //---------------------------------------------------------------------
        public Font  HelpFont                   = Font.UiStatistics;

        public float HelpSize                   = 0.75f;

        public Color HelpColor                  = Color.White;

        public MotivationItemSettings()
        {
        }
    }
}