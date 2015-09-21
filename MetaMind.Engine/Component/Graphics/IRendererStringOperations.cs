namespace MetaMind.Engine.Component.Graphics
{
    using Font;
    using Microsoft.Xna.Framework;

    public interface IRendererStringOperations
    {
        /// <summary>
        /// Draws the left-top mono-spaced text at particular position.
        /// </summary>
        void DrawMonospacedString(string fontName, string str, Vector2 position, Color color, float scale);

        void DrawMonospacedString(string fontName, string str, Vector2 position, Color color, float scale, HoritonalAlignment HAlignment, VerticalAlignment VAlignment, int leading = 0);

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        void DrawString(string fontName, string str, Vector2 position, Color color, float scale);

        void DrawString(string fontName, string str, Vector2 position, Color color, float scale, HoritonalAlignment HAlignment, VerticalAlignment VAlignment, int leading = 0);
    }
}