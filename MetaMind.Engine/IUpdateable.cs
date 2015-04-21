namespace MetaMind.Engine
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IUpdateable : Microsoft.Xna.Framework.IUpdateable
    {
        void UpdateInterop(IGameInteropService interop, GameTime time);
    }
}