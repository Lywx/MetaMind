// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MotivationItemSettings.cs" company="Cogn">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MetaMind.Perseverance.Guis.Widgets.Motivations.Items
{
    using MetaMind.Engine.Components;
    using MetaMind.Engine.Guis.Elements.Items;

    using Microsoft.Xna.Framework;

    /// <summary>
    ///     The motivation item settings.
    /// </summary>
    public class MotivationItemSettings : ItemSettings
    {
        // ---------------------------------------------------------------------
        #region Fields

        /// <summary>
        ///     The fear color.
        /// </summary>
        public Color FearColor = new Color(51, 204, 204, 223);

        /// <summary>
        ///     The id pending color.
        /// </summary>
        public Color IdPendingColor = new Color(200, 200, 0, 2);

        /// <summary>
        ///     The name color.
        /// </summary>
        public Color NameColor = Color.White;

        /// <summary>
        ///     The name font.
        /// </summary>
        public Font NameFont = Font.InfoSimSunFont;

        /// <summary>
        ///     The name frame size.
        /// </summary>
        public Point NameFrameSize = new Point(256, 34);

        /// <summary>
        ///     The name line margin.
        /// </summary>
        public int NameLineMargin = 20;

        /// <summary>
        ///     The name size.
        /// </summary>
        public float NameSize = 0.6f;

        /// <summary>
        ///     The symbol frame increment factor.
        /// </summary>
        public float SymbolFrameIncrementFactor = 0.2f;

        /// <summary>
        ///     The wish color.
        /// </summary>
        public Color WishColor = new Color(255, 255, 255, 128);

        #endregion
    }
}