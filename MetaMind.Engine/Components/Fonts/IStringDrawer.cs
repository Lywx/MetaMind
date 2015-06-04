namespace MetaMind.Engine.Components.Fonts
{
    using Microsoft.Xna.Framework;

    public interface IStringDrawer : IGameComponent
    {
        /// <summary>
        /// Draws the left-top monospaced text at particular position.
        /// </summary>
        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale);

        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale, StringHAlign hAlign, StringVAlign vAlign, int leading = 0);

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        void DrawString(Font font, string str, Vector2 position, Color color, float scale);

        void DrawString(Font font, string str, Vector2 position, Color color, float scale, StringHAlign hAlign, StringVAlign vAlign, int leading = 0);
    }
}