namespace MetaMind.Engine.Components.Fonts
{
    using Microsoft.Xna.Framework;

    public interface IStringDrawer : IGameComponent
    {
        /// <summary>
        /// Draws the left-top monospaced text at particular position.
        /// </summary>
        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale);

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        void DrawString(Font font, string str, Vector2 position, Color color, float scale);

        /// <summary>
        /// Draws text horizontally centered at particular position.
        /// </summary>
        void DrawStringCenteredH(Font font, string str, Vector2 position, Color color, float scale);

        /// <summary>
        /// Draws text both horizontally and vertically centered at particular position.
        /// </summary>
        void DrawStringCenteredHV(Font font, string str, Vector2 position, Color color, float scale);

        /// <summary>
        /// Draws text vertically centered at particular position.
        /// </summary>
        void DrawStringCenteredV(Font font, string str, Vector2 position, Color color, float scale);
    }
}