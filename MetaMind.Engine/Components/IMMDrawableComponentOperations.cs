namespace MetaMind.Engine.Components
{
    using Microsoft.Xna.Framework;

    public interface IMMDrawableComponentOperations
    {
        void BeginDraw(GameTime time);

        void Draw(GameTime time);

        void EndDraw(GameTime time);
    }
}