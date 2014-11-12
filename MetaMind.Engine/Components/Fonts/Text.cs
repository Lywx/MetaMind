// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Text.cs" company="UESTC">
//   Copyright (c) 2014 Lin Wuxiang
//   All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MetaMind.Engine.Components.Fonts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MetaMind.Engine.Extensions;

    using Microsoft.Xna.Framework.Graphics;

    public class Text : EngineObject
    {
        /// <summary>
        /// break text using word by word method.
        /// </summary>
        public static string BreakTextIntoLines(SpriteFont font, string text, float maxLineWidth)
        {
            var stringBuilder = new StringBuilder();
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                var words = line.Split(' ');
                var lineWidth = 0f;
                var spaceWidth = font.MeasureString(" ").X;
                foreach (var word in words)
                {
                    var size = font.MeasureString(word);
                    if (lineWidth + size.X < maxLineWidth)
                    {
                        stringBuilder.Append(word + " ");
                        lineWidth += size.X + spaceWidth;
                    }
                    else
                    {
                        stringBuilder.Append("\n" + word + " ");
                        lineWidth = size.X + spaceWidth;
                    }
                }

                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }

        public static List<string> BreakTextIntoList(Font font, float size, string text, float maxLineWidth)
        {
            return BreakTextIntoList(FontManager[font], size, text, maxLineWidth);
        }

        /// <summary>
        /// Break text using letter by letter method.
        /// </summary>
        public static List<string> BreakTextIntoList(SpriteFont font, float size, string text, float maxLineWidth)
        {
            var textList = new List<string>();
            var line = new StringBuilder();
            var lineWidth = 0;
            var index = 0;
            while (index < text.Length)
            {
                while (lineWidth < maxLineWidth)
                {
                    // add current letter
                    line.Append(text[index]);

                    // measure length and decide whether to go into next line
                    if (NextCharExist(text, index))
                    {
                        lineWidth = (int)(font.MeasureString(string.Concat(line.ToString(), text[index + 1])).X * size);
                        index++;
                    }
                    else
                    {
                        lineWidth = (int)(font.MeasureString(line).X * size);
                        index++;
                        break;
                    }
                }

                textList.Add(line.ToString());

                // initialize next line
                if (NextCharExist(text, index))
                {
                    if (NextCharContinuous(text, index - 1))
                    {
                        line = new StringBuilder("-");
                        lineWidth = 0;
                    }
                    else
                    {
                        line = new StringBuilder();
                        lineWidth = 0;
                    }
                }
                else
                {
                    break;
                }
            }

            return textList;
        }

        public static string ChopText(string text, int maxLength)
        {
            return text.Length < maxLength ? text : string.Concat(text.Substring(0, maxLength), "...");
        }
        private static bool NextCharContinuous(string text, int index)
        {
            var firstLetter = text.Substring(index, 1);
            var firstChar = text[index];
            var secondChar = text[index + 1];
            return firstLetter.IsAscii() && !char.IsWhiteSpace(firstChar) && !char.IsWhiteSpace(secondChar);
        }

        private static bool NextCharExist(string text, int index)
        {
            return index + 1 < text.Count();
        }
    }
}