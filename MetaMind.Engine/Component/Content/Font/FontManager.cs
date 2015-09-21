// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Font.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Component.Content.Font
{
    using System.Collections.Generic;
    using Asset;
    using Content;

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : GameControllableComponent, IFontManager
    {
        #region Dependency

        private IAssetManager Asset => this.Interop.Asset;

        #endregion

        #region Font 

        public Dictionary<string, FontAsset> Fonts { get; private set; } = new Dictionary<string, FontAsset>(); 

        #endregion

        #region Constructors

        public FontManager(GameEngine engine)
            : base(engine)
        {
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            FontName.Initialize(this);
        }

        #endregion

        #region Load and Unload

        /// <summary>
        /// Adds the fonts from assets.
        /// </summary>
        protected override void LoadContent()
        {
            foreach (var font in this.Asset.Fonts)
            {
                this.Fonts[font.Name] = font;
            }
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