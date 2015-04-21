namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    using IGameInteropService = MetaMind.Engine.IGameInteropService;

    public interface IModule : IInputable, IDrawable
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }

        void Load(IGameInteropService interop, IGameInputService input, Services.IGameInteropService interop);

        void Unload(IGameInteropService interop, IGameInputService input, Services.IGameInteropService interop);
    }
}