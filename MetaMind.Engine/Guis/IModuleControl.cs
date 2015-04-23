namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IModuleControl : IUpdateable, IInputable
    {
        void LoadContent(IGameInteropService interop);

        void UnloadContent(IGameInteropService interop);
    }
}