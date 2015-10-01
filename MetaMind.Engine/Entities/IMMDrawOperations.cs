namespace MetaMind.Engine.Entities
{
    using Microsoft.Xna.Framework;
    using Service;

    public interface IMMDrawOperations
    {
        void BeginDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha);

        void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha);

        void EndDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha);
    }
}
