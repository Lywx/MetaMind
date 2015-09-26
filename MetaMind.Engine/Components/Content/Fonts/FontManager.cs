// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Font.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Content.Fonts
{
    using System.Collections.Generic;
    using Extensions;

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : GameControllableComponent, IFontManager
    {
        #region Font 

        public Font this[string index] => this.Fonts[index];

        private Dictionary<string, Font> Fonts { get; set; } = new Dictionary<string, Font>();

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

            StringUtils.Initialize(this);
        }

        #endregion

        #region Load and Unload

        protected override void LoadContent()
        {
        }

        public void Add(FontAsset fontAsset)
        {
            if (!this.Fonts.ContainsKey(fontAsset.Name))
            {
                this.Fonts.Add(fontAsset.Name, new Font(fontAsset));
            }
        }

        public void Remove(FontAsset fontAsset)
        {
            if (this.Fonts.ContainsKey(fontAsset.Name))
            {
                this.Fonts.Remove(fontAsset.Name);
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