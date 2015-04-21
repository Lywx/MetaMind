namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    public interface IModule : IInputable, IDrawable
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }

        void Load(IGameInputService input, IGameInteropService interop);

        void Unload(IGameInputService input, IGameInteropService interop);
    }
}