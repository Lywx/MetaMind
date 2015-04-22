namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    public interface IModuleControl : IUpdateable, IInputable
    {
        void Load(IGameInteropService interop);

        void Unload(IGameInteropService interop);
    }
}