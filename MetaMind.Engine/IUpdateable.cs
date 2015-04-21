namespace MetaMind.Engine
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IUpdateable : Microsoft.Xna.Framework.IUpdateable
    {
        void UpdateContent(IGameFile gameFile, GameTime time);

        void UpdateInterop(IGameInteropService interop, GameTime time);
    }
}