// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Font.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;

    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : DrawableGameComponent, IFontManager
    {
        #region Font Data

        public Dictionary<Font, FontInfo> Fonts { get; set; }

        #endregion

        #region Dependency

        private IGameInteropService Interop { get; set; }

        #endregion

        #region Contructors

        public FontManager(GameEngine engine)
            : base(engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("engine");
            }

            engine.Components.Add(this);
        }

        #endregion

        public override void Initialize()
        {
            // It has to register service before LoadContent.
            this.Interop = GameEngine.Service.Interop;

            base.Initialize();

            FontExt.Initialize(this);
        }

        #region Load and Unload

        /// <summary>
        /// Load the fonts from the content pipeline.
        /// </summary>
        protected override void LoadContent()
        {
            this.LoadFont(Font.UiRegular,    18, @"Fonts/UiRegularFont");
            this.LoadFont(Font.UiStatistics, 18, @"Fonts/UiStatisticsFont");
            this.LoadFont(Font.UiConsole   , 18, @"Fonts/UiConsoleFont");

            this.LoadFont(Font.ContentRegular,    18, @"Fonts/NSimSunRegularFont");
            this.LoadFont(Font.ContentBold,       18, @"Fonts/NSimSunBoldFont");
            this.LoadFont(Font.ContentItalic,     18, @"Fonts/NSimSunItalicFont");
            this.LoadFont(Font.ContentBoldItalic, 18, @"Fonts/NSimSunBoldItalicFont");
        }

        private void LoadFont(Font font, int fontSize, string path)
        {
            if (this.Fonts == null)
            {
                this.Fonts = new Dictionary<Font, FontInfo>();
            }

            var spriteFont = this.Interop.Content.Load<SpriteFont>(path);

            this.Fonts[font] = new FontInfo(font, spriteFont, fontSize);
        }

        /// <summary>
        /// Release all references to the fonts.
        /// </summary>
        protected override void UnloadContent()
        {
            this.Fonts.Clear();
        }

        #endregion Load and Unload
    }
}