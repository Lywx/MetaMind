namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework;
    using Services;

    public interface IMMDrawOperations
    {
        void BeginDraw(IMMEngineGraphicsService graphics, GameTime time);

        void Draw(IMMEngineGraphicsService graphics, GameTime time);

        void EndDraw(IMMEngineGraphicsService graphics, GameTime time);
    }
}
