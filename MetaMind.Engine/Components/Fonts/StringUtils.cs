namespace MetaMind.Engine.Components.Fonts
{
    using System;
    using System.Text;

    public static class StringUtils
    {
        #region String Cropping
        
        public static string CropMonospacedString(string str, float scale, int maxLength)
        {
            // HACK: May not use standard font here
            return CropString(Font.ContentRegular, str, scale, maxLength, true);
        }

        public static string CropMonospacedStringByAsciiCount(string str, int count)
        {
            // HACK: May not use standard font here
            return CropMonospacedString(str, 1.0f, (int)(count * Font.ContentRegular.GetMono().AsciiSize(1.0f).X));
        }

        public static string CropString(Font font, string str, float scale, int maxLength, bool monospaced = false)
        {
            if (maxLength < 1)
            {
                throw new ArgumentOutOfRangeException("maxLength");
            }

            var stringCropped = font.PrintableString(str);
            var stringSize    = font.MeasureString(stringCropped, scale, monospaced);

            var isCropped = false;
            var isOutOfRange = stringSize.X > maxLength;

            while (isOutOfRange)
            {
                isCropped = true;
                
                stringCropped = stringCropped.Substring(0, stringCropped.Length - 1);
                stringSize    = font.MeasureString(stringCropped, scale, monospaced);

                isOutOfRange = stringSize.X > maxLength;
            }

            if (isCropped)
            {
                return CropStringTail(stringCropped);
            }

            return stringCropped;
        }

        private static string CropStringTail(string str)
        {
            if (str.Length > 2)
            {
                var head = str.Substring(0, str.Length - 3);
                var tail = str.Substring(str.Length - 1 - 3, 3);

                return head + (string.IsNullOrWhiteSpace(tail) ? "   " : "...");
            }

            return str;
        }

        #endregion 

        #region Breaking

        /// <summary>
        /// Break string using word by word method.
        /// </summary>
        public static string BreakStringByWord(Font font, string str, float scale, float maxLineWidth, bool monospaced)
        {
            var spaceWidth = font.MeasureString(" ", scale, monospaced).X;

            var result = new StringBuilder();

            var lines = str.Split('\n');
            foreach (var line in lines)
            {
                var words = line.Split(' ');
                var lineWidth = 0f;
                foreach (var word in words)
                {
                    var size = font.MeasureString(word, scale, monospaced);
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

        #endregion
    }
}