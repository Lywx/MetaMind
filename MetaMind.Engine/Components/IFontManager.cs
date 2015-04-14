namespace MetaMind.Engine.Components
{
    using System.Collections.Generic;

    using MetaMind.Engine.Components.Fonts;

    using Microsoft.Xna.Framework;

    public interface IFontManager
    {
        /// <summary>
        /// Draws the left-top monospaced text at particular position.
        /// </summary>
        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale);

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="str">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="scale">The scale.</param>
        /// <exception cref="System.ArgumentNullException">Font is not initialized.</exception>
        void DrawString(Font font, string str, Vector2 position, Color color, float scale);

        void DrawStringCenteredH(Font font, string str, Vector2 position, Color color, float scale);

        /// <summary>
        /// Draws text centered at particular position.
        /// </summary>
        void DrawStringCenteredHV(Font font, string str, Vector2 position, Color color, float scale);

        void DrawStringCenteredV(Font font, string str, Vector2 position, Color color, float scale);

        Vector2 MeasureMonospacedString(string str, float scale);

        Vector2 MeasureString(Font font, string str, float scale);

        string CropMonospacedString(string str, float scale, int maxLength);

        string CropMonospacedStringByAsciiCount(string str, int count);

        string CropString(Font font, string str, float scale, int maxLength);

        string CropString(Font font, string str, float scale, int maxLength, bool monospaced);

        int GetCJKExclusiveCharCount(string str);

        string GetDisaplayableString(Font font, string str);

        List<int> GetNonDisaplayableCharIndexes(Font font, string str);
    }
}