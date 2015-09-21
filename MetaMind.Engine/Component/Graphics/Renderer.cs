namespace MetaMind.Engine.Component.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Font;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Renderer : IRenderer
    {
        #region Dependency

        private readonly SpriteBatch spriteBatch;

        #endregion

        #region Constructors

        public Renderer(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            this.spriteBatch = spriteBatch;
        }

        #endregion

        #region Initialization

        public void Initialize()
        {
        }

        #endregion

        #region Draw

        public void DrawMonospacedString(string fontName, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var font = fontName.ToFont();
            var displayable = font.AvailableString(str);

            var CJKCharIndexes = displayable.CJKUniqueCharIndexes();
            var CJKCharAmendedPosition = this.CJKUniqueCharAmendedPosition(CJKCharIndexes, displayable);

            var isCJKCharExisting = CJKCharIndexes.Count > 0;

            for (var i = 0; i < displayable.Length; ++i)
            {
                var charPosition = isCJKCharExisting ? CJKCharAmendedPosition[i] : i;
                var amendedPosition = position + new Vector2(charPosition * font.MonoData.AsciiSize(scale).X, 0);

                this.DrawMonospacedChar(fontName, displayable[i], amendedPosition, color, scale);
            }
        }

        /// <param name="leading">Vertical distance from line to line</param>
        public void DrawMonospacedString(string fontName, string str, Vector2 position, Color color, float scale, HoritonalAlignment HAlignment, VerticalAlignment VAlignment, int leading = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var font = fontName.ToFont();

            if (leading == 0)
            {
                leading = (int)font.MonoData.AsciiSize(scale).Y * 2;
            }

            var lines = str.Split('\n');

            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];

                var lineString = font.AvailableString(line);
                var lineSize = font.MeasureMonospacedString(lineString, scale);

                var linePosition = position;

                linePosition += new Vector2(0, i * leading);

                if (VAlignment == VerticalAlignment.Center)
                {
                    linePosition -= new Vector2(0, lineSize.Y / 2);
                }

                if (VAlignment == VerticalAlignment.Top)
                {
                    linePosition -= new Vector2(0, lineSize.Y);
                }

                if (HAlignment == HoritonalAlignment.Center)
                {
                    linePosition -= new Vector2(lineSize.X / 2, 0);
                }

                if (HAlignment == HoritonalAlignment.Left)
                {
                    linePosition -= new Vector2(lineSize.X, 0);
                }

                this.DrawMonospacedString(fontName, lineString, linePosition, color, scale);
            }
        }

        private void DrawMonospacedChar(string fontName, char c, Vector2 position, Color color, float scale)
        {
            var font = fontName.ToFont();
            var str = c.ToString(CultureInfo.InvariantCulture);

            position += font.MonoData.Offset + new Vector2(-font.MeasureString(str, scale).X / 2, 0);

            this.DrawString(fontName, str, position, color, scale);
        }

        public void DrawString(string fontName, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var font = fontName.ToFont();

            this.spriteBatch.DrawString(font.SpriteData, font.AvailableString(str), position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawString(string fontName, string str, Vector2 position, Color color, float scale, HoritonalAlignment HAlignment, VerticalAlignment VAlignment, int leading = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var font = fontName.ToFont();

            if (leading == 0)
            {
                leading = (int)font.MonoData.AsciiSize(scale).Y * 2;
            }

            var lines = str.Split('\n');

            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];

                var lineString = font.AvailableString(line);
                var lineSize = font.MeasureString(lineString, scale);

                var linePosition = position;

                linePosition += new Vector2(0, i * leading);

                if (VAlignment == VerticalAlignment.Center)
                {
                    linePosition -= new Vector2(0, lineSize.Y / 2);
                }

                if (VAlignment == VerticalAlignment.Top)
                {
                    linePosition -= new Vector2(0, lineSize.Y);
                }

                if (HAlignment == HoritonalAlignment.Center)
                {
                    linePosition -= new Vector2(lineSize.X / 2, 0);
                }

                if (HAlignment == HoritonalAlignment.Left)
                {
                    linePosition -= new Vector2(lineSize.X, 0);
                }

                this.DrawString(fontName, lineString, linePosition, color, scale);
            }
        }

        #endregion

        #region CJK Char Helper

        private List<float> CJKUniqueCharAmendedPosition(List<int> CJKUniqueCharIndexes, string str)
        {
            var position = 0f;
            var indexes = new List<float>();

            for (var i = 0; i < str.Length; i++)
            {
                if (CJKUniqueCharIndexes.Contains(i))
                {
                    position += 0.5f;
                }

                if (i > 0 && CJKUniqueCharIndexes.Contains(i - 1))
                {
                    position += 0.5f;
                }

                indexes.Add(position);
                position += 1f;
            }

            return indexes;
        }

        #endregion
    }
}