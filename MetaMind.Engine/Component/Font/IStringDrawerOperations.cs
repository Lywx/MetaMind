namespace MetaMind.Engine.Component.Font
{
    using Microsoft.Xna.Framework;

    public interface IStringDrawerOperations
    {
        /// <summary>
        /// Draws the left-top mono-spaced text at particular position.
        /// </summary>
        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale);

        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale, HoritonalAlignment HAlignment, VerticalAlignment VAlignment, int leading = 0);

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        void DrawString(Font font, string str, Vector2 position, Color color, float scale);

        void DrawString(Font font, string str, Vector2 position, Color color, float scale, HoritonalAlignment HAlignment, VerticalAlignment VAlignment, int leading = 0);
    }
}