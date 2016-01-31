namespace MetaMind.Engine.Core.Entity.Common
{
    using Microsoft.Xna.Framework;

    public interface IMMDrawableOperations
    {
        void BeginDraw(GameTime time);

        void Draw(GameTime time);

        void EndDraw(GameTime time);
    }
}
