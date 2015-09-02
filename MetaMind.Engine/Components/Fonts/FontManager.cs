// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Font.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Fonts
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Services;

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : DrawableGameComponent, IFontManager
    {
        #region Font Data

        public Dictionary<Font, FontInfo> Fonts { get; set; } = new Dictionary<Font, FontInfo>(); 

        #endregion

        #region Dependency

        private IGameInteropService GameInterop { get; set; }

        #endregion

        #region Contructors

        public FontManager(GameEngine engine)
            : base(engine)
        {
        }

        #endregion

        public override void Initialize()
        {
            // It has to register service before LoadContent. Because 
            // this.LoadContent use GameEngine.Service.Interop to load fonts
            this.GameInterop = GameEngine.Service.Interop;

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
            var spriteFont = this.GameInterop.Content.Load<SpriteFont>(path);

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

        #region IDisposable

        private bool IsDisposed { get; set; }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (!this.IsDisposed)
                    {
                        // SpriteFont is not disposable
                        this.UnloadContent();

                        this.GameInterop = null;
                    }

                    this.IsDisposed = true;
                }
            }
            catch
            {
                // Ignored
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #endregion
    }
}