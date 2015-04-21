namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    using IUpdateable = MetaMind.Engine.IUpdateable;

    public interface IModuleControl : IUpdateable, IInputable
    {
        void Load(IGameInputService input, IGameInteropService interop);

        void Unload(IGameInputService input, IGameInteropService interop);
    }

    public abstract class ModuleControl<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleControl
        where                           TModule                                   : Module         <TModuleSettings>
        where                           TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleControl(TModule module)
            : base(module)
        {
        }

        public abstract void Load(IGameInputService input, IGameInteropService interop);

        public abstract void Unload(IGameInputService input, IGameInteropService interop);
    }
}