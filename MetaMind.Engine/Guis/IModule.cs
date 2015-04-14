namespace MetaMind.Engine.Guis
{
    public interface IModule : IInputable, IDrawable
    {
        IModuleControl Control { get; }

        IModuleGraphics Graphics { get; }

        void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio);

        void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio);
    }
}