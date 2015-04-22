namespace MetaMind.Engine.Guis
{
    using MetaMind.Engine.Services;

    using IUpdateable = MetaMind.Engine.IUpdateable;

    public interface IModuleControl : IUpdateable, IInputable
    {
        void Load(IGameInteropService interop);

        void Unload(IGameInteropService interop);
    }

    public abstract class ModuleControl<TModule, TModuleSettings, TModuleControl> : ModuleComponent<TModule, TModuleSettings, TModuleControl>, IModuleControl
        where                           TModule                                   : Module         <TModuleSettings>
        where                           TModuleControl                            : ModuleControl  <TModule, TModuleSettings, TModuleControl>
    {
        protected ModuleControl(TModule module)
            : base(module)
        {
        }

        public abstract void Load(IGameInteropService interop);

        public abstract void Unload(IGameInteropService interop);
    }
}