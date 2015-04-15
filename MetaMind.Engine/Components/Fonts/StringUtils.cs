namespace MetaMind.Engine.Components.Fonts
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StringUtils
    {
        #region Breaking

        /// <summary>
        /// Break string using word by word method.
        /// </summary>
        public static string BreakStringIntoLinesByWord(Font font, string str, float maxLineWidth)
        {
            var spaceWidth = font.MeasureString(" ", 1f).X;

            var result = new StringBuilder();

            var lines = str.Split('\n');
            foreach (var line in lines)
            {
                var words = line.Split(' ');
                var lineWidth = 0f;
                foreach (var word in words)
                {
                    var size = font.MeasureString(word, 1f);
                    if (lineWidth + size.X < maxLineWidth)
                    {
                        result.Append(word + " ");
                        lineWidth += size.X + spaceWidth;
                    }
                    else
                    {
                        result.Append("\n" + word + " ");
                        lineWidth = size.X + spaceWidth;
                    }
                }

                result.Append("\n");
            }

            return result.ToString();
        }

        /// <summary>
        /// Break string using letter by letter method.
        /// </summary>
        public static List<string> BreakStringIntoListByLetter(Font font, string str, float scale, float maxLineWidth)
        {
            var stringList = new List<string>();
            var line = new StringBuilder();
            var lineWidth = 0;
            var index = 0;
            while (index < str.Length)
            {
                while (lineWidth < maxLineWidth)
                {
                    // add current letter
                    line.Append(str[index]);

                    // measure length and decide whether to go into next line
                    if (IsNextCharExisting(str, index))
                    {
                        lineWidth = (int)font.MeasureString(string.Concat(line.ToString(), str[index + 1]), scale).X;
                        index++;
                    }
                    else
                    {
                        lineWidth = (int)font.MeasureString(line.ToString(), scale).X;
                        index++;
                        break;
                    }
                }

                stringList.Add(line.ToString());

                // initialize next line
                if (IsNextCharExisting(str, index))
                {
                    if (IsNextCharContinuous(str, index - 1))
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

            return stringList;
        }

        private static bool IsNextCharContinuous(string text, int index)
        {
            var firstChar = text[index];
            var secondChar = text[index + 1];

            return firstChar.ToString(CultureInfo.InvariantCulture).IsAscii() && 
                !char.IsWhiteSpace(firstChar) && 
                !char.IsWhiteSpace(secondChar);
        }

        private static bool IsNextCharExisting(string text, int index)
        {
            return index + 1 < text.Count();
        }

        #endregion
    }
}