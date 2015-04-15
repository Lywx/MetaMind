namespace MetaMind.Engine.Components.Fonts
{
    using System;

    using Microsoft.Xna.Framework;

    public class StringProcessor : IStringProcessor
    {
        #region String Measuring

        private Vector2 MeasureString(Font font, string str, float scale, bool monospaced)
        {
            if (monospaced)
            {
                return font.MeasureMonospacedString(str, scale);
            }

            return font.MeasureString(str, scale);
        }

        #endregion

        #region String Cropping
        
        public string CropMonospacedString(string str, float scale, int maxLength)
        {
            // HACK: May not use standard font here
            return this.CropString(Font.ContentRegular, str, scale, maxLength, true);
        }

        public string CropMonospacedStringByAsciiCount(string str, int count)
        {
            // HACK: May not use standard font here
            return this.CropMonospacedString(str, 1.0f, (int)(count * Font.ContentRegular.GetMono().AsciiSize(1.0f)));
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

            var stringCropped = font.DisaplayableString(str);
            var stringSize    = this.MeasureString(font, stringCropped, scale, monospaced);

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

        #endregion 
    }
}