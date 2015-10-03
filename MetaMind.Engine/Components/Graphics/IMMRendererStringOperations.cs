namespace MetaMind.Engine.Components.Graphics
{
    using Content.Fonts;
    using Gui.Graphics.Fonts;
    using Microsoft.Xna.Framework;

    public interface IMMRendererStringOperations
    {
        /// <summary>
        /// Draws the left-top mono-spaced text at particular position.
        /// </summary>
        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale);

        void DrawMonospacedString(Font font, string str, Vector2 position, Color color, float scale, HoritonalAlignment halignment, VerticalAlignment valignment, int leading = 0);

        /// <summary>
        /// Draws the left-top text at particular position.
        /// </summary>
        void DrawString(Content.Fonts.Font font, string str, Vector2 position, Color color, float scale);

        void DrawString(Content.Fonts.Font font, string str, Vector2 position, Color color, float scale, HoritonalAlignment halignment, VerticalAlignment valignment, int leading = 0);
    }
}