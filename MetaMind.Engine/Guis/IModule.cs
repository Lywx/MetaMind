namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    public interface IModule : IInputable, IDrawable
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }

        void Load(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio);

        void Unload(IGameFile gameFile, IGameInputService input, IGameInteropService interop, IGameAudioService audio);
    }
}