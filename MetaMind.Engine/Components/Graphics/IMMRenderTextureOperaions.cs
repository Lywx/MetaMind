namespace MetaMind.Engine.Components.Graphics
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMRenderTextureOperaions
    {
        void Draw(Texture2D texture, Rectangle destination, Rectangle source, Color color, float depth);

        void Draw(Texture2D texture, Rectangle destination, Color color, float depth);

        void Draw(Texture2D texture, int x, int y, Rectangle source, Color color, float depth);

        void Draw(Texture2D texture, int x, int y, Color color, float depth);
    }
}