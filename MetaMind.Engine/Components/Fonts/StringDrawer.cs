namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public enum StringHAlign { Left, Center, Right }

    public enum StringVAlign { Top, Center, Bottom }

    public class StringDrawer : IStringDrawer
    {
        #region Dependency

        private SpriteBatch SpriteBatch { get; set; }

        #endregion

        #region Constructors

        public StringDrawer(SpriteBatch spriteBatch)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }

            this.SpriteBatch = spriteBatch;
        }

        #endregion

        #region IGameComponent

        public void Initialize()
        {
        }

        #endregion

        #region IStringDrawer

        public void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            var displayable = font.PrintableString(str);

            var CJKCharIndexes = displayable.CJKExclusiveCharIndexes();
            var CJKCharAmendedPosition = this.CJKExclusiveCharAmendedPosition(CJKCharIndexes, displayable);

            var isCJKCharExisting = CJKCharIndexes.Count > 0;

            for (var i = 0; i < displayable.Length; ++i)
            {
                var charPosition = isCJKCharExisting ? CJKCharAmendedPosition[i] : i;
                var amendedPosition = position + new Vector2(charPosition * font.Mono().AsciiSize(scale).X, 0);

                this.DrawMonospacedChar(font, displayable[i], amendedPosition, color, scale);
            }
        }

        /// <param name="leading">Vertical distance from line to line</param>
        public void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale, StringHAlign HAlign, StringVAlign VAlign, int leading = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            if (leading == 0)
            {
                leading = (int)font.Mono().AsciiSize(scale).Y * 2;
            }

            var lines = str.Split('\n');

            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];

                var lineString = font.PrintableString(line);
                var lineSize = font.MeasureMonospacedString(lineString, scale);

                var linePosition = position;

                linePosition += new Vector2(0, i * leading);

                if (VAlign == StringVAlign.Center)
                {
                    linePosition -= new Vector2(0, lineSize.Y / 2);
                }

                if (VAlign == StringVAlign.Top)
                {
                    linePosition -= new Vector2(0, lineSize.Y);
                }

                if (HAlign == StringHAlign.Center)
                {
                    linePosition -= new Vector2(lineSize.X / 2, 0);
                }

                if (HAlign == StringHAlign.Left)
                {
                    linePosition -= new Vector2(lineSize.X, 0);
                }

                this.DrawMonospacedString(font, lineString, linePosition, color, scale);
            }
        }

        public void DrawString(Font font, string str, Vector2 position, Color color, float scale)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            SpriteBatch.DrawString(font.Sprite(), font.PrintableString(str), position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void DrawString(Font font, string str, Vector2 position, Color color, float scale, StringHAlign HAlign, StringVAlign VAlign, int leading = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            if (leading == 0)
            {
                leading = (int)font.Mono().AsciiSize(scale).Y * 2;
            }

            var lines = str.Split('\n');

            for (var i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];

                var lineString = font.PrintableString(line);
                var lineSize = font.MeasureString(lineString, scale);

                var linePosition = position;

                linePosition += new Vector2(0, i * leading);

                if (VAlign == StringVAlign.Center)
                {
                    linePosition -= new Vector2(0, lineSize.Y / 2);
                }

                if (VAlign == StringVAlign.Top)
                {
                    linePosition -= new Vector2(0, lineSize.Y);
                }

                if (HAlign == StringHAlign.Center)
                {
                    linePosition -= new Vector2(lineSize.X / 2, 0);
                }

                if (HAlign == StringHAlign.Left)
                {
                    linePosition -= new Vector2(lineSize.X, 0);
                }

                this.DrawString(font, lineString, linePosition, color, scale);
            }
        }

        private void DrawMonospacedChar(Font font, char c, Vector2 position, Color color, float scale)
        {
            var str = c.ToString(CultureInfo.InvariantCulture);

            position += new Vector2(font.Mono().FontMargin - font.MeasureString(str, scale).X / 2, 0);

            this.DrawString(font, str, position, color, scale);
        }

        #endregion

        #region CJK Char Helper

        private List<float> CJKExclusiveCharAmendedPosition(List<int> CJKExclusiveCharIndexes, string str)
        {
            var position = 0f;
            var indexes = new List<float>();

            for (var i = 0; i < str.Length; i++)
            {
                if (CJKExclusiveCharIndexes.Contains(i))
                {
                    position += 0.5f;
                }

                if (i > 0 && CJKExclusiveCharIndexes.Contains(i - 1))
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