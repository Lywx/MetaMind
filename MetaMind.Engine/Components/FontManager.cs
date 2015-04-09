// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Font.cs" company="UESTC">
//   Copyright (c) 2014 Wuxiang Lin
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using MetaMind.Engine.Components.Fonts;
    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Static storage of SpriteFont objects and colors for use throughout the game.
    /// </summary>
    public class FontManager : IGameEngineComponent
    {
        #region Monospaced Font Data

        private const float MonospacedFontFactor = 2f / 3f;

        // this is a margin prefix to monospaced string to left-parallel normally spaced string
        private const int MonospacedFontMargin = 7;

        private const int MonospacedFontSize = 18;

        private float MonospacedFontAsciiSize(float scale)
        {
            return MonospacedFontSize * MonospacedFontFactor * scale;
        }

        #endregion 

        #region Font Indexer

        private static readonly SpriteFont[] fonts = new SpriteFont[(int)Font.FontNum];

        public SpriteFont this[Font font]
        {
            get
            {
                return fonts[(int)font];
            }

            private set
            {
                fonts[(int)font] = value;
            }
        }

        #endregion Font Indexer

        #region Singleton

        private static FontManager Singleton { get; set; }

        public static FontManager GetInstance()
        {
            return Singleton ?? (Singleton = new FontManager());
        }

        #endregion Singleton

        #region Load and Unload

        /// <summary>
        /// Load the fonts from the content pipeline.
        /// </summary>
        public void LoadContent()
        {
            this[Font.UiRegularFont]    = GameEngine.ContentManager.Load<SpriteFont>(@"Fonts/BitmapFonts/RegularFont");
            this[Font.UiStatisticsFont] = GameEngine.ContentManager.Load<SpriteFont>(@"Fonts/BitmapFonts/StatisticsFont");

            this[Font.UiContentFont]    = GameEngine.ContentManager.Load<SpriteFont>(@"Fonts/SpriteFonts/NSimSunFont");
        }

        /// <summary>
        /// Release all references to the fonts.
        /// </summary>
        public void UnloadContent()
        {
        }

        #endregion Load and Unload

        #region String Drawing

        /// <summary>
        /// Draws the left-top monospaced text at particular position.
        /// </summary>
        public void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var stringDisplayable = this.GetDisaplayableString(font, str);

            var cjkCharIndexes         = this.GetCJKExclusiveCharIndexes(stringDisplayable);
            var cjkCharAmendedPosition = this.GetCJKExclusiveCharPositionAmendments(cjkCharIndexes, stringDisplayable);

            var isCJKCharExisting = cjkCharIndexes.Count > 0;

            for (var i = 0; i < stringDisplayable.Length; ++i)
            {
                var charPosition = isCJKCharExisting ? cjkCharAmendedPosition[i] : i;
                var amendedPosition = position + new Vector2(charPosition * this.MonospacedFontAsciiSize(scale), 0);

                this.DrawMonospacedChar(font, stringDisplayable[i], amendedPosition, color, scale);
            }
        }

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="str">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="scale">The scale.</param>
        /// <exception cref="System.ArgumentNullException">Font is not initialized.</exception>
        public void DrawString(Font font, string str, Vector2 position, Color color, float scale)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            GameEngine.ScreenManager.SpriteBatch.DrawString(GameEngine.FontManager[font], this.GetDisaplayableString(font, str), position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawStringCenteredH(Font font, string str, Vector2 position, Color color, float scale)
        {
            position += new Vector2(0, font.MeasureString(str).Y * scale / 2);

            this.DrawStringCenteredHV(font, str, position, color, scale);
        }

        /// <summary>
        /// Draws text centered at particular position.
        /// </summary>
        public void DrawStringCenteredHV(Font font, string str, Vector2 position, Color color, float scale)
        {
            // check for trivial text
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var stringDisplayable = this.GetDisaplayableString(font, str);
            var stringSize = this.MeasureString(font, stringDisplayable, scale);

            position -= new Vector2((int)(stringSize.X / 2), (int)(stringSize.Y / 2));

            this.DrawString(font, stringDisplayable, position, color, scale);
        }

        public void DrawStringCenteredV(Font font, string str, Vector2 position, Color color, float scale)
        {
            position += new Vector2(font.MeasureString(str).X * scale / 2, 0);

            this.DrawStringCenteredHV(font, str, position, color, scale);
        }

        private void DrawMonospacedChar(Font font, char khar, Vector2 position, Color color, float scale)
        {
            var str = khar.ToString(CultureInfo.InvariantCulture);

            position += new Vector2(MonospacedFontMargin - this.MeasureString(font, str, scale).X / 2, 0);

            this.DrawString(font, str, position, color, scale);
        }

        #endregion Text Drawing

        #region String Measurement

        public Vector2 MeasureMonospacedString(string str, float scale)
        {
            var cjkCharCount = this.GetCJKExclusiveCharCount(str);
            var asciiCharCount = str.Length - cjkCharCount;

            var monoSize = this.MonospacedFontAsciiSize(scale);

            return new Vector2((asciiCharCount + cjkCharCount * 2) * monoSize, monoSize);
        }

        public Vector2 MeasureString(Font font, string str, float scale)
        {
            return this.MeasureString(font, str, scale, false);
        }

        private Vector2 MeasureString(Font font, string str, float scale, bool monospaced)
        {
            if (monospaced)
            {
                return this.MeasureMonospacedString(str, scale);
            }

            return font.MeasureString(str, scale);
        }

        #endregion

        #region String Cropping
        
        public string CropMonospacedString(string str, float scale, int maxLength)
        {
            return this.CropString(Font.UiContentFont, str, scale, maxLength, true);
        }

        public string CropMonospacedStringByAsciiCount(string str, int count)
        {
            return this.CropMonospacedString(str, 1.0f, (int)(count * this.MonospacedFontAsciiSize(1.0f)));
        }

        public string CropString(Font font, string str, float scale, int maxLength)
        {
            return this.CropString(font, str, scale, maxLength, false);
        }

        public string CropString(Font font, string str, float scale, int maxLength, bool monospaced)
        {
            if (maxLength < 1)
            {
                throw new ArgumentOutOfRangeException("maxLength");
            }

            var stringDisaplayable = this.GetDisaplayableString(font, str);
            var stringCropped      = stringDisaplayable;
            var stringSize         = this.MeasureString(font, stringCropped, scale, monospaced);

            var isCropped = false;
            var isOutOfRange = stringSize.X > maxLength;

            while (isOutOfRange)
            {
                isCropped = true;
                
                stringCropped = stringCropped.Substring(0, stringCropped.Length - 1);
                stringSize    = this.MeasureString(font, stringCropped, scale, monospaced);

                isOutOfRange = stringSize.X > maxLength;
            }

            if (isCropped)
            {
                return this.CropStringTail(stringCropped);
            }

            return stringCropped;
        }

        private string CropStringTail(string str)
        {
            if (str.Length > 2)
            {
                var head = str.Substring(0, str.Length - 3);
                var tail = str.Substring(str.Length - 1 - 3, 3);

                return head + (string.IsNullOrWhiteSpace(tail) ? "   " : "...");
            }

            return str;
        }

        #endregion Text Cropping

        #region String Filtering

        public int GetCJKExclusiveCharCount(string str)
        {
            return this.GetCJKExclusiveCharIndexes(str).Count;
        }

        public string GetDisaplayableString(Font font, string str)
        {
            return this.GetDisaplayableString(GameEngine.FontManager[font], str);
        }

        public List<int> GetNonDisaplayableCharIndexes(Font font, string str)
        {
            return this.GetNonDisaplayableCharIndexes(GameEngine.FontManager[font], str);
        }

        private List<int> GetCJKExclusiveCharIndexes(string str)
        {
            return this.GetNonDisaplayableCharIndexes(Font.UiRegularFont, str);
        }

        private List<float> GetCJKExclusiveCharPositionAmendments(List<int> cjkExclusiveCharIndexes, string str)
        {
            var position = 0f;
            var indexes  = new List<float>();

            for (var i = 0; i < str.Length; i++)
            {
                if (cjkExclusiveCharIndexes.Contains(i))
                {
                    position += 0.5f;
                }

                if (i > 0 && cjkExclusiveCharIndexes.Contains(i - 1))
                {
                    position += 0.5f;
                }

                indexes.Add(position);
                position += 1f;
            }

            return indexes;
        }

        private string GetCJKInclusiveString(string str)
        {
            return this.GetDisaplayableString(Font.UiContentFont, str);
        }

        private string GetDisaplayableString(SpriteFont font, string str)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            return str.Where(t => font.Characters.Contains(t)).Aggregate(string.Empty, (current, t) => current + t);
        }

        private List<int> GetNonDisaplayableCharIndexes(SpriteFont font, string str)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }

            var indexes = new List<int>();
            for (var i = 0; i < str.Length; i++)
            {
                if (!font.Characters.Contains(str[i]))
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        #endregion Text Filtering
    }
}