namespace MetaMind.Engine.Guis
{
    using IUpdateable = MetaMind.Engine.IUpdateable;

    public interface IModuleControl : IUpdateable, IInputable
    {
        void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio);

        void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio);
    }

    public abstract class ModuleControl<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleControl
        where                           TModule                                   : Module         <TModuleSettings>
        where                           TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleControl(TModule module)
            : base(module)
        {
        }

        public abstract void Load(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio);

        public abstract void Unload(IGameFile gameFile, IGameInput gameInput, IGameInterop gameInterop, IGameAudio gameAudio);
    }
}