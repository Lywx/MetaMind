namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    using Microsoft.Xna.Framework;

    public interface IModuleLogicControl : IUpdateable, IInputable
    {
        void LoadContent(IGameInteropService interop);

        void UnloadContent(IGameInteropService interop);
    }
}