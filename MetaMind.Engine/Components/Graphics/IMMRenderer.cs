namespace MetaMind.Engine.Components.Graphics
{
    using Content.Fonts;
    using Entities;
    using Entities.Graphics.Fonts;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IMMRendererTextureOperaions
    {
        void Draw(
            Texture2D texture,
            Rectangle destination,
            Rectangle source,
            Color color,
            float depth);

        void Draw(
            Texture2D texture,
            Rectangle destination,
            Color color,
            float depth);

        void Draw(
            Texture2D texture,
            int x,
            int y,
            Rectangle source,
            Color color,
            float depth);

        void Draw(Texture2D texture, int x, int y, Color color, float depth);

        void DrawImmediate(
            Texture2D texture,
            Vector2? position = null,
            Rectangle? destinationRectangle = null,
            Rectangle? sourceRectangle = null,
            Vector2? origin = null,
            float rotation = 0f,
            Vector2? scale = null,
            Color? color = null,
            SpriteEffects effects = SpriteEffects.None,
            float depth = 0f,
            Matrix? transformation = null);

        void DrawImmediate(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float depth,
            Matrix transformation);

        void DrawImmediate(
            Texture2D texrture,
            Rectangle destinationRectangle,
            Rectangle sourceRectangle,
            Color color);
    }

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
        void DrawString(Font font, string str, Vector2 position, Color color, float scale);

        void DrawString(Font font, string str, Vector2 position, Color color, float scale, HoritonalAlignment halignment, VerticalAlignment valignment, int leading = 0);
    }

    public interface IMMRendererBatchOperations
    {
        void Begin();

        void Begin(
            BlendState blendState,
            SamplerState samplerState = null,
            DepthStencilState depthStencilState = null,
            RasterizerState rasterizerState = null,
            Effect effect = null,
            Matrix? transformMatrix = null);

        void End();
    }

    public interface IMMRendererBase : IMMReactor
    {
        
    }

    public interface IMMRenderer : IMMRendererBase,
        IMMRendererStringOperations,
        IMMRendererTextureOperaions,
        IMMRendererBatchOperations
    {
    }
}