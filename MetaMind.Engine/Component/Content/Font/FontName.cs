// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExt.cs" company="UESTC">
//   Copyright (c) 2015 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.Content.Font
{
    using System;

    public static class FontName
    {
        #region Dependency

        private static IFontManager Manager { get; set; }

        #endregion

        #region Initialization

        public static void Initialize(IFontManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            Manager = manager;
        }

        #endregion

        #region Font

        internal static FontAsset ToFont(this string font) => Manager.Fonts[font];
        #endregion
    }
}