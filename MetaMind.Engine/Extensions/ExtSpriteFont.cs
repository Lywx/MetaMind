namespace MetaMind.Engine.Extensions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal static class ExtSpriteFont
    {
        public static Vector2 MeasureString(this SpriteFont spriteFont, string text, float scale)
        {
            return spriteFont.MeasureString(text) * scale;
        }
    }
}