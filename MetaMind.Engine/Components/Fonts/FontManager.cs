// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Font.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Fonts;

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

        #region Engine Data

        private IGameGraphics GameGraphics { get; set; }

        private IGameFile GameFile { get; set; }

        #endregion

        #region Singleton

        private static FontManager Singleton { get; set; }

        public static FontManager GetInstance(GameEngine gameEngine)
        {
            if (Singleton == null)
            {
                Singleton = new FontManager(gameEngine);
            }

            if (gameEngine != null)
            {
                gameEngine.Components.Add(Singleton);
            }

            return Singleton;
        }

        #endregion Singleton

        #region Contructors

        public FontManager(GameEngine gameEngine)
            : base(gameEngine)
        {
            this.GameFile     = new GameEngineFile(gameEngine);
            this.GameGraphics = new GameEngineGraphics(gameEngine);
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            FontExt.Initialize(this);
        }

        #region Load and Unload

        /// <summary>
        /// Load the fonts from the content pipeline.
        /// </summary>
        protected override void LoadContent()
        {
            this.LoadFont(Font.UiRegular,    18, @"Fonts/BitmapFonts/RegularFont");
            this.LoadFont(Font.UiStatistics, 18, @"Fonts/BitmapFonts/StatisticsFont");

            this.LoadFont(Font.ContentRegular,    18, @"Fonts/SpriteFonts/NSimSunRegularFont");
            this.LoadFont(Font.ContentBold,       18, @"Fonts/SpriteFonts/NSimSunBoldFont");
            this.LoadFont(Font.ContentItalic,     18, @"Fonts/SpriteFonts/NSimSunItalicFont");
            this.LoadFont(Font.ContentBoldItalic, 18, @"Fonts/SpriteFonts/NSimSunBoldItalicFont");
        }

        private void LoadFont(Font font, int fontSize, string path)
        {
            if (this.Fonts == null)
            {
                this.Fonts = new Dictionary<Font, FontInfo>();
            }

            var spriteFont = this.GameFile.Content.Load<SpriteFont>(path);

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