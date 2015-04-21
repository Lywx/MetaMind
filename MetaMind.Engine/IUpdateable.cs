namespace MetaMind.Engine
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IUpdateable : Microsoft.Xna.Framework.IUpdateable
    {
        void UpdateContent(IGameInteropService interop, GameTime time);

        void UpdateInterop(IGameInteropService interop, GameTime time);
    }
}