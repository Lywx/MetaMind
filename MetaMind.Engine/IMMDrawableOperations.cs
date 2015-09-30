namespace MetaMind.Engine
{
    using Microsoft.Xna.Framework;
    using Service;

    public interface IMMDrawableOperations
    {
        void BeginDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha);

        void Draw(IMMEngineGraphicsService graphics, GameTime time, byte alpha);

        void EndDraw(IMMEngineGraphicsService graphics, GameTime time, byte alpha);
    }
}
