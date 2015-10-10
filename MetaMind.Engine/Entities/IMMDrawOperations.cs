namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework;

    public interface IMMDrawOperations
    {
        void BeginDraw(GameTime time);

        void Draw(GameTime time);

        void EndDraw(GameTime time);
    }
}
